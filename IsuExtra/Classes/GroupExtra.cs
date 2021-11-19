using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Classes
{
    public class GroupExtra : Group
    {
        private const int MaxNumberOfStudentsInGroup = 5;

        public GroupExtra(GroupName groupName, List<Student> students, List<GroupLesson> timeTable)
            : base(groupName, students)
        {
            TimeTable = timeTable;
            MegaFaculty = groupName.Name[0];
            StudentsExtra = new List<StudentExtra>();
        }

        public GroupExtra(GroupName groupName)
            : base(groupName, new List<Student>())
        {
            TimeTable = new List<GroupLesson>();
            MegaFaculty = groupName.Name[0];
            StudentsExtra = new List<StudentExtra>();
        }

        public List<GroupLesson> TimeTable { get; set; }
        public List<StudentExtra> StudentsExtra { get; set; }
        public char MegaFaculty { get; set; }

        public void AddDisciplineToTimeTable(GroupLesson groupLesson)
        {
            TimeTable.Add(groupLesson);
        }

        public bool CheckIfGroupHasFreePlaces()
        {
            return StudentsExtra.Count >= MaxNumberOfStudentsInGroup;
        }

        public StudentExtra AddStudent(StudentExtra student)
        {
            StudentsExtra.Add(student);
            return student;
        }

        public void RemoveStudentFromGroup(StudentExtra student)
        {
            StudentsExtra.Remove(student);
        }
    }
}