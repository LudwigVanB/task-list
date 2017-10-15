using System;
using System.Collections.Generic;

namespace Tasks
{
    public class Project
    {

        public Project(ProjectId id)
        {
            Id = id;
        }

        public ProjectId Id {get; private set;}

        public string Format()
        {
            return Id.Format();
        }        

        public IList<Task> Tasks { get; private set; } = new List<Task>();

    }

}
