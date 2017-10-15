using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    public class ProjectRepository
    {
        public TaskId.TaskIdGenerator IdGenerator { get; private set; } = new TaskId.TaskIdGenerator();

        public void AddProject(ProjectId projectId)
        {
            _projects[projectId] = new Project(projectId);
        }

        public void AddTask(ProjectId projectId, Task task)
        {
            var project = _projects[projectId];
            if (project == null)
            {
                Console.WriteLine("Could not find a project with the name \"{0}\".", project);
                return;
            }
            project.Tasks.Add(task);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _projects.Values;
        }

        public Task GetTask(TaskId id)
        {
            return _projects.Values
                .Select(project => project.Tasks.FirstOrDefault(task => task.Id == id))
                .Where(task => task != null)
                .FirstOrDefault();
        }

        public IEnumerable<Task> GetTaskByDeadline(Deadline deadline)
        {
            return _projects.Values.SelectMany(project => project.Tasks.Where(task => task.Deadline == deadline));
        }

        public IEnumerable<Task> GetTasksByDate()
        {
            return _projects.Values.SelectMany(project => project.Tasks).OrderBy(task => task.Deadline);
        }

        public void DeleteTask(TaskId id)
        {
            foreach (var project in _projects.Values)
            {
                foreach (var task in project.Tasks)
                {
                    if (task.Id == id)
                    {
                        project.Tasks.Remove(task);
                        return;
                    }
                }
            }
        }

        private readonly IDictionary<ProjectId, Project> _projects = new Dictionary<ProjectId, Project>();
    }
}
