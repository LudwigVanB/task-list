using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    public class Project
    {

        public Project(string name)
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
            var otherProject = obj as Project;
            if (otherProject == null) return false;
            return _name == otherProject._name;
        }

        public bool Equals(Project otherProject)
        {
            if (otherProject == null) return false;
            return _name == otherProject._name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
        #endregion

        private string _name;


    }

}
