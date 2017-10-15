namespace Tasks.commands
{
    public class AddProjectCommand : Command
    {
        public AddProjectCommand(string args) : base(args)
        {
            _projectId = new ProjectId(args);
        }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            repository.AddProject(_projectId);
        }

        public static new string GetArgsHelp()
        {
            return " <project name>";
        }

        private ProjectId _projectId;
    }
}