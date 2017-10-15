using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tasks.commands
{
    public class CommandParser
    {
        public CommandParser()
        {
        }

        public Command Parse(string commandLine, IConsole console)
        {
            var commandParts = commandLine.Split(COMMAND_SEPARATOR,2);
            var commandName = commandParts[0];
            var commandRests = commandParts.Length > 1 ? commandParts[1] : null;
            Type commandType;
            while (!_nameToCommandClass.TryGetValue(commandName, out commandType) && commandRests != null)
            {
                var commandRestsParts = commandRests.Split(COMMAND_SEPARATOR, 2);
                commandName = $"{commandName}{COMMAND_SEPARATOR[0]}{commandRestsParts[0]}";
                commandRests = commandRestsParts.Length > 1 ? commandRestsParts[1] : null;
            }
            if (commandType != null)
            {
                return (Command)Activator.CreateInstance(commandType, new object[] { commandRests });
            }
            console.WriteLine("I don't know what the command \"{0}\" is.", commandName);
            return null;
        }

        public void WriteHelp(IConsole console)
        {
            console.WriteLine("Commands:");
            foreach (var commandPair in _nameToCommandClass)
            {
                var commandType = commandPair.Value;
                var methodInfo = commandType.GetMethod("GetArgsHelp", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static);
                var argsHelpString = (string)methodInfo.Invoke(null, null);
                var helpLine = "  " + commandPair.Key + argsHelpString;
                console.WriteLine(helpLine);
            }
            console.WriteLine("");
        }

        private readonly char[] COMMAND_SEPARATOR = " ".ToCharArray();

        private readonly Dictionary<string, Type> _nameToCommandClass = new Dictionary<string, Type>()
        {
            { "add project", typeof(AddProjectCommand) },
            { "add task", typeof(AddTaskCommand) },
            { "check", typeof(CheckCommand) },
            { "uncheck", typeof(UncheckCommand) },
            { "deadline", typeof(DeadlineCommand) },
            { "today", typeof(TodayCommand) },
            { "view by project", typeof(ViewByProjectCommand) },
            { "view by date", typeof(ViewByDateCommand) },
            { "view by deadline", typeof(ViewByDateCommand) },
            { "delete", typeof(DeleteCommand) },
            { "help", typeof(HelpCommand) },
        };
    }
}