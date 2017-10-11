using System;
using System.Collections.Generic;

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
            Type commandType;
            if (_nameToCommandClass.TryGetValue(commandName, out commandType))
            {
                var commandRests = commandParts.Length > 1 ? commandParts[1] : null;
                return (Command)Activator.CreateInstance(commandType, new object[] { commandRests } );
            }
            return null;
        }

        private readonly char[] COMMAND_SEPARATOR = " ".ToCharArray();

        private readonly Dictionary<string, Type> _nameToCommandClass = new Dictionary<string, Type>()
        {
            { "deadline", typeof(DeadlineCommand) },
            { "today", typeof(TodayCommand) }
        };
    }
}