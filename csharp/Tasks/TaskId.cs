using System;
using System.Text.RegularExpressions;

namespace Tasks
{
    public class TaskId
    {
        public TaskId(string id)
        {
            _id = id;
        }

        public static TaskId NewId(TaskIdGenerator idGenerator, string id = null)
        {
            var newTaskId = new TaskId("0");
            if (id==null)
            {
                newTaskId._id = idGenerator.NextId();
            }
            else
            {
                var regexp = new Regex(@"\W");
                var safeId = regexp.Replace(id, "_");
                newTaskId._id = safeId;
                idGenerator.UpdateLastId(newTaskId._id);
            }
            return newTaskId;
        }

        #region Equals
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
        #endregion

        public string Format()
        {
            return _id;
        }

        private string _id;

        public class TaskIdGenerator
        {
            private long _lastId = 0;

            internal string NextId()
            {
                return (++_lastId).ToString();
            }

            internal void UpdateLastId(string id)
            {
                if (long.TryParse(id, out long longId))
                {
                    _lastId = Math.Max(_lastId, longId);
                }
            }

        }
    }
}
