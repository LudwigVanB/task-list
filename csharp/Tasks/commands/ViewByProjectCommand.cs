namespace Tasks.commands
{
    internal class ViewByProjectCommand : Command
    {
        public ViewByProjectCommand(string args) : base(args)
        {
        }

        public override void Execute(TaskList taskList, IConsole console)
        {
            foreach (var project in taskList.GetProjects())
            {
                console.WriteLine(project.Format());
                foreach (var task in taskList.GetTasksByProject(project))
                {
                    console.WriteLine(task.Format());
                }
                console.WriteLine();
            }
        }
    }
}