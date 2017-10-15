using System;

namespace Tasks.commands
{
    public class DeadlineCommand : Command
    {
        public DeadlineCommand(string args) : base(args)
        {
            var argsParts = args.Split(ARGS_SEPARATOR, 2);
            TaskId = new TaskId(argsParts[0]);
            Deadline = new Deadline(argsParts[1]);
        }

        public TaskId TaskId { get; private set; }

        public Deadline Deadline { get; private set; }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            var task = repository.GetTask(TaskId);
            if (task == null)            
                console.WriteLine("Could not find a task with an ID of {0}.", task.Id.Format());
            else
                task.Deadline = Deadline;
        }

        public static new string GetArgsHelp()
        {
            return " <task ID> <date as xxxx-xx-xx>";
        }
    }
}