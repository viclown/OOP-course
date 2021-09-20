using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const int MaxNumberOfStudentsInGroup = 5;

        public Group(GroupName groupName, List<Student> students)
        {
            GroupName = groupName;
            CourseNumber = groupName.GetCourseNumber();
            Students = students;
        }

        public Group(GroupName groupName)
            : this(groupName, new List<Student>()) { }

        public GroupName GroupName { get; private set; }
        public CourseNumber CourseNumber { get; private set; }
        public List<Student> Students { get; private set; }

        public bool CheckIfGroupIsFull()
        {
            return Students.Count + 1 > MaxNumberOfStudentsInGroup;
        }

        public Student FindStudentInGroup(string name)
        {
            foreach (Student student in Students)
            {
                if (student.Name == name) return student;
            }

            throw new StudentIsNotInGroupException();
        }
    }
}