using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string ProjectName { get; set; }
        
        public string ProjectDescription { get; set; }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return ProjectName == other.ProjectName;
        }

        public override int GetHashCode()
        {
            return ProjectName.GetHashCode();
        }

        public override string ToString()
        {
            return "name= " + ProjectName ;
        }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return ProjectName.CompareTo(other.ProjectName);
        }
    }
}
