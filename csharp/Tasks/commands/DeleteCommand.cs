namespace Tasks.commands
{
    public class DeleteCommand : Command
    {
        public DeleteCommand(string args) : base(args)
        {
            _taskId = new TaskId(args);
        }

        public override void Execute(TaskList taskList, IConsole console)
        {
            taskList.DeleteTask(_taskId);
        }

        public static new string GetArgsHelp()
        {
            return " <task ID>";
        }

        private TaskId _taskId;

        
    }
}