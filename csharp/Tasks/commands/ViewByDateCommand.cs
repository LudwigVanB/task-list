namespace Tasks.commands
{
    public class ViewByDateCommand : Command
    {
        public ViewByDateCommand(string args) : base(args)
        {
        }

        public override void Execute(TaskList taskList, IConsole console)
        {
            var orderedTasks = taskList.GetTasksByDate();
            Deadline previousDate = null;
            foreach (var task in orderedTasks)
            {
                if (task.Deadline != previousDate)
                {
                    if (previousDate != null)
                    {
                        console.WriteLine();
                    }
                    previousDate = task.Deadline;
                    console.WriteLine(task.Deadline.Format(noDate: "No date"));
                }
                console.WriteLine(task.Format(showDeadline:false));
            }
            console.WriteLine();
        }
    }
}