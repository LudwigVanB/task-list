using System;

namespace Tasks.commands
{
    class TodayCommand : Command
    {
        public TodayCommand(string args) : base(args)
        {
        }

        public override void Execute(ProjectRepository repository, IConsole console)
        {
            var todayDeadline = new Deadline(DateTime.Today);
            var todayTasks = repository.GetTaskByDeadline(todayDeadline);
            foreach (var task in todayTasks)
            {
                console.WriteLine(task.Format());
            }
            console.WriteLine();
        }
    }
}
