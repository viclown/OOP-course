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
            CourseNumber = GetCourseNumber(groupName);
            Students = students.AsReadOnly();
            StudentsList = students;
        }

        public Group(GroupName groupName)
            : this(groupName, new List<Student>()) { }

        public GroupName GroupName { get; }
        public CourseNumber CourseNumber { get; }
        public IList<Student> Students { get; }
        private List<Student> StudentsList { get; }
        private List<CourseNumber> Courses { get; } = new List<CourseNumber>();

        public bool CheckIfGroupIsFull()
        {
            return Students.Count >= MaxNumberOfStudentsInGroup;
        }

        public Student FindStudentInGroup(string name)
        {
            foreach (Student student in Students)
            {
                if (student.Name == name) return student;
            }

            throw new StudentIsNotInGroupException();
        }

        public void AddStudentToGroup(Student student)
        {
            StudentsList.Add(student);
        }

        public void RemoveStudentFromGroup(Student student)
        {
            StudentsList.Remove(student);
        }

        private CourseNumber GetCourseNumber(GroupName groupName)
        {
            char curCourse = groupName.Name[2];
            foreach (CourseNumber courseNumber in Courses)
            {
                if (curCourse == courseNumber.Number)
                    return courseNumber;
            }

            var newCourse = new CourseNumber(groupName.Name[2]);
            Courses.Add(newCourse);
            return new CourseNumber(groupName.Name[2]);
        }
    }
}