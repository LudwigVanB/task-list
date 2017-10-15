using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    public class ProjectId
    {
        public ProjectId(string name)
        {
            _name = name;
        }
       
        public string Format()
        {
            return _name;
        }

        #region Equals
        public override bool Equals(Object obj)
        {
            var otherProjectId = obj as ProjectId;
            if (otherProjectId == null) return false;
            return _name == otherProjectId._name;
        }

        public bool Equals(ProjectId otherProjectId)
        {
            if (otherProjectId == null) return false;
            return _name == otherProjectId._name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
        #endregion

        private string _name;

    }
}
