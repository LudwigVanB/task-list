namespace Tasks.commands
{
    public class AddTaskCommand : Command
    {
        public AddTaskCommand(string args) : base(args)
        {
            var argsParts = args.Split(ARGS_SEPARATOR, 2);
            _project = new Project(argsParts[0]);
            if (argsParts[1].StartsWith(ID_PREFIX))
            {
                argsParts = argsParts[1].Split(ARGS_SEPARATOR, 2);
                _userTaskId = argsParts[0].Substring(ID_PREFIX.Length);
            }
            _description = argsParts[1];
        }

        public override void Execute(TaskList taskList, IConsole console)
        {
            TaskId taskId = TaskId.NewId(taskList.IdGenerator, _userTaskId);
            var task = new Task { Id = taskId, Description = _description, Done = false };
            taskList.AddTask(_project, task);
        }

        public static new string GetArgsHelp()
        {
            return " <project name> [id:<task id>] <task description>";
        }

        private const string ID_PREFIX = "id:";

        private string _userTaskId; 
        private Project _project;
        private string _description;

    }
}
