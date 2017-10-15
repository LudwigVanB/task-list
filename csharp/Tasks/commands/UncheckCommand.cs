namespace Tasks.commands
{
    public class UncheckCommand : CheckCommand
    {
        public UncheckCommand(string args) : base(args)
        {
        }

        protected override void SetDone(Task task)
        {
            task.Done = false;
        }
    }
}