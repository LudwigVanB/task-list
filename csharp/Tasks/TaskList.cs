using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.commands;

namespace Tasks
{
	public sealed class TaskList
	{
		private const string QUIT = "quit";

		private readonly IDictionary<string, IList<Task>> tasks = new Dictionary<string, IList<Task>>();
		private readonly IConsole console;

        public TaskId.TaskIdGenerator IdGenerator { get; private set; } = new TaskId.TaskIdGenerator();

		public static void Main(string[] args)
		{
			new TaskList(new RealConsole()).Run();
		}

		public TaskList(IConsole console)
		{
			this.console = console;
		}

		public void Run()
		{
			while (true) {
				console.Write("> ");
				var command = console.ReadLine();
				if (command == QUIT) {
					break;
				}
				Execute(command);
			}
		}

		private void Execute(string commandLine)
		{
            var parser = new CommandParser();
            var commandObject = parser.Parse(commandLine);
            if (commandObject != null)
            {
                commandObject.Execute(this, console);
                return;
            }
			var commandRest = commandLine.Split(" ".ToCharArray(), 2);
			var command = commandRest[0];
			switch (command) {
			case "show":
				Show();
				break;
			case "add":
				Add(commandRest[1]);
				break;
			case "check":
				Check(commandRest[1]);
				break;
			case "uncheck":
				Uncheck(commandRest[1]);
				break;
			case "help":
				Help();
				break;
			default:
				Error(command);
				break;
			}
		}

		private void Show()
		{
			foreach (var project in tasks) {
				console.WriteLine(project.Key);
				foreach (var task in project.Value) {
                    console.WriteLine(task.Format());					
				}
				console.WriteLine();
			}
		}

		private void Add(string commandLine)
		{
			var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
			var subcommand = subcommandRest[0];
			if (subcommand == "project") {
				AddProject(subcommandRest[1]);
			} else {
                Error($"add {subcommand}");                
            }
		}

		private void AddProject(string name)
		{
			tasks[name] = new List<Task>();
		}

        public void AddTask(string project, Task task)
        {
            IList<Task> projectTasks = tasks[project];
            if (projectTasks == null)
            {
                Console.WriteLine("Could not find a project with the name \"{0}\".", project);
                return;
            }
            projectTasks.Add(task);
        }

        private void Check(string idString)
		{
			SetDone(idString, true);
		}

		private void Uncheck(string idString)
		{
			SetDone(idString, false);
		}

        public Task GetTask(TaskId id)
        {
            var identifiedTask = tasks
                .Select(project => project.Value.FirstOrDefault(task => task.Id == id))
                .Where(task => task != null)
                .FirstOrDefault();
            if (identifiedTask == null)
            {
                console.WriteLine("Could not find a task with an ID of {0}.", id);
                return null;
            }
            return identifiedTask;
        }

        public IEnumerable<Task> GetTaskByDeadline(Deadline deadline)
        {
            return tasks.SelectMany(project => project.Value.Where(task => task.Deadline == deadline));
        }

		private void SetDone(string idString, bool done)
		{
			var id = new TaskId(idString);
			var identifiedTask = tasks
				.Select(project => project.Value.FirstOrDefault(task => task.Id == id))
				.Where(task => task != null)
				.FirstOrDefault();
			if (identifiedTask == null) {
				console.WriteLine("Could not find a task with an ID of {0}.", id);
				return;
			}

			identifiedTask.Done = done;
		}

		private void Help()
		{
            var commandParser = new CommandParser();
			console.WriteLine("Commands:");
			console.WriteLine("  show");
			console.WriteLine("  add project <project name>");
			console.WriteLine("  check <task ID>");
			console.WriteLine("  uncheck <task ID>");
            commandParser.WriteHelp(console);
			console.WriteLine();
		}

		private void Error(string command)
		{
			console.WriteLine("I don't know what the command \"{0}\" is.", command);
		}
	}
}
