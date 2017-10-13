using System;
using System.Collections.Generic;

namespace Tasks
{
	public class Task
	{
		public TaskId Id { get; set; }

		public string Description { get; set; }

		public bool Done { get; set; }

        public Deadline Deadline { get; internal set; } = Deadline.NO_DEADLINE;

        public string Format()
        {
            return string.Format("    [{0}] {1}: {2}{3}",
                (Done ? 'x' : ' '),
                Id.Format(),
                Description,
                Deadline.Format() );
        }
    }
}
