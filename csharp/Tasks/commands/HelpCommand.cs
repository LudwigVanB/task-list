namespace Tasks.commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(string args) : base(args)
        {
        }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            new CommandParser().WriteHelp(console);
        }
    }
}