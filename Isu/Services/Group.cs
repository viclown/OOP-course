using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        public Group(GroupName groupName, List<Student> students)
        {
            GroupName = groupName;
            CourseNumber = groupName.GetCourseNumber();
            Students = students;
        }

        public Group(GroupName groupName)
            : this(groupName, new List<Student>()) { }

        public GroupName GroupName { get; set;  }
        public CourseNumber CourseNumber { get; set;  }
        public List<Student> Students { get; set; }
    }
}