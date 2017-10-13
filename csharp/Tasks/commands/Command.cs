using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.commands
{
    public abstract class Command
    {
        public Command(string args) { }

        public abstract void Execute(TaskList taskList, IConsole console);

        public static string GetArgsHelp()
        {
            return "";
        }

        protected readonly char[] ARGS_SEPARATOR = " ".ToCharArray();
    }
}
