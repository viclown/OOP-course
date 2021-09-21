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

        public GroupName GroupName { get; }
        public CourseNumber CourseNumber { get; }
        public List<Student> Students { get; }

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