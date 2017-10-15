namespace Tasks.commands
{
    public class CheckCommand : Command
    {
        public CheckCommand(string args) : base(args)
        {
            _taskId = new TaskId(args);
        }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            var task = repository.GetTask(_taskId);
            if (task == null)
                console.WriteLine("Could not find a task with an ID of {0}.", task.Id.Format());
            else
                SetDone(task);
        }

        public static new string GetArgsHelp()
        {
            return " <task ID>";
        }

        protected virtual void SetDone(Task task)
        {
            task.Done = true;
        }

        private TaskId _taskId;
    }
}