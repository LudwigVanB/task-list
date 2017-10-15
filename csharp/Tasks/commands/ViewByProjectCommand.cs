namespace Tasks.commands
{
    internal class ViewByProjectCommand : Command
    {
        public ViewByProjectCommand(string args) : base(args)
        {
        }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            foreach (var project in repository.GetProjects())
            {
                console.WriteLine(project.Format());
                foreach (var task in project.Tasks)
                {
                    console.WriteLine(task.Format());
                }
                console.WriteLine();
            }
        }
    }
}