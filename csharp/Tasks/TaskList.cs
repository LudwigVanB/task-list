using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.commands;

namespace Tasks
{
	public sealed class TaskList
	{
		private const string QUIT = "quit";

        private readonly ProjectRepository _repository = new ProjectRepository();
        private readonly IConsole _console;
        private readonly CommandParser _commandParser = new CommandParser();

        public static void Main(string[] args)
		{
			new TaskList(new RealConsole()).Run();
		}

		public TaskList(IConsole console)
		{
            _console = console;
		}

		public void Run()
		{
			while (true) {
				_console.Write("> ");
				var command = _console.ReadLine();
				if (command == QUIT) {
					break;
				}
				Execute(command);
			}
		}

		private void Execute(string commandLine)
		{
            var commandObject = _commandParser.Parse(commandLine, _console);
            if (commandObject != null)
            {
                commandObject.Execute(_repository, _console);               
            }            			
		}			
        
	}
}
