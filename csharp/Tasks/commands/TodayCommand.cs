using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.commands
{
    class TodayCommand : Command
    {
        public TodayCommand(string args) : base(args)
        {
        }

        public override void Execute(TaskList taskList, IConsole console)
        {
            var todayDeadline = new Deadline(DateTime.Today);
            var todayTasks = taskList.GetTaskByDeadline(todayDeadline);
            foreach (var task in todayTasks)
            {
                console.WriteLine(task.Format());
            }
            console.WriteLine();
        }
    }
}
