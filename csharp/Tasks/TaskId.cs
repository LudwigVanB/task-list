using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    class TaskId
    {
        public TaskId(string id)
        {
            _id = id;
        }

        public override bool Equals(Object obj)
        {
            var otherId = obj as TaskId;
            if (otherId == null) return false;
            return _id == otherId._id;
        }

        public bool Equals(TaskId otherId)
        {
            if (otherId == null) return false;
            return _id == otherId._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public static bool operator ==(TaskId a, TaskId b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            return a._id == b._id;
        }

        public static bool operator !=(TaskId a, TaskId b)
        {
            return !(a == b);
        }

        private string _id;

    }
}
