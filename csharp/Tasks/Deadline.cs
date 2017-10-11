using System;

namespace Tasks
{
    public class Deadline
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

        public string Format()
        {
            return _date == null ? "" : $" Due {_date?.ToString("yyyy-MM-dd")}";
        }

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


        private Deadline()
        {
        }

        private DateTime? _date;        
    }
}
