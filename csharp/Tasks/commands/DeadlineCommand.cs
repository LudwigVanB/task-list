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

        public override void Execute(TaskList taskList, IConsole console)
        {
            var task = taskList.GetTask(TaskId);
            task.Deadline = Deadline;
        }

        public static new string GetArgsHelp()
        {
            return " <task ID> <date as xxxx-xx-xx>";
        }
    }
}