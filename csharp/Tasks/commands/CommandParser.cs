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

        public Command Parse(string commandLine)
        {
            var commandParts = commandLine.Split(COMMAND_SEPARATOR,2);
            var commandName = commandParts[0];
            var commandRests = commandParts.Length > 1 ? commandParts[1] : null;
            Type commandType;
            while(!_nameToCommandClass.TryGetValue(commandName, out commandType) && commandRests != null)
            {
                var commandRestsParts = commandRests.Split(COMMAND_SEPARATOR, 2);
                commandName = $"{commandName}{COMMAND_SEPARATOR[0]}{commandRestsParts[0]}";
                commandRests = commandRestsParts.Length > 1 ? commandRestsParts[1] : null;
            }
            if (commandType != null)
            {
                return (Command)Activator.CreateInstance(commandType, new object[] { commandRests });
            }
            return null;
        }

        public void WriteHelp(IConsole console)
        {
            foreach (var commandPair in _nameToCommandClass)
            {
                var commandType = commandPair.Value;
                var methodInfo = commandType.GetMethod("GetArgsHelp", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static);
                var argsHelpString = (string)methodInfo.Invoke(null, null);
                var helpLine = "  " + commandPair.Key + argsHelpString;
                console.WriteLine(helpLine);
            }
        }

        private readonly char[] COMMAND_SEPARATOR = " ".ToCharArray();

        private readonly Dictionary<string, Type> _nameToCommandClass = new Dictionary<string, Type>()
        {
            { "add task", typeof(AddTaskCommand) },
            { "deadline", typeof(DeadlineCommand) },
            { "today", typeof(TodayCommand) },
            { "view by project", typeof(ViewByProjectCommand) },
            { "view by date", typeof(ViewByDateCommand) },
            { "view by deadline", typeof(ViewByDateCommand) },
            { "delete", typeof(DeleteCommand) }
        };
    }
}