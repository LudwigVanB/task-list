using System;

namespace Tasks
{
    public class Deadline : IComparable<Deadline>, IComparable
    {
        public static readonly Deadline NO_DEADLINE = new Deadline();

        public Deadline(string isoDate)
        {
            _date = DateTime.Parse(isoDate);
        }


        public Deadline(DateTime date)
        {
            _date = date;
        }

        public string Format(string prefix = "", string noDate = "")
        {
            return _date == null ? noDate : $"{prefix}{_date?.ToString("yyyy-MM-dd")}";
        }

        #region Equals
        public override bool Equals(Object obj)
        {
            var otherDeadline = obj as Deadline;
            if (otherDeadline == null) return false;
            return _date == otherDeadline._date;
        }

        public bool Equals(Deadline otherDeadline)
        {
            if (otherDeadline == null) return false;
            return _date == otherDeadline._date;
        }

        public override int GetHashCode()
        {
            return _date?.GetHashCode() ?? 0;
        }
        
        public static bool operator ==(Deadline a, Deadline b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            return a._date == b._date;
        }

        public static bool operator !=(Deadline a, Deadline b)
        {
            return !(a == b);
        }
        #endregion

        public int CompareTo(Deadline other)
        {
            if (_date == null && other._date == null) return 0;
            else if (_date == null) return 1;
            else if (other._date == null) return -1;
            else return _date.Value.CompareTo(other._date.Value);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Deadline)obj);
        }

        private Deadline()
        {
        }

        private DateTime? _date;        
    }
}
