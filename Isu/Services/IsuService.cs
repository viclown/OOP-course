using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService
    {
        public IsuService()
        {
            LastId = 100000;
        }

        private int LastId { get; set; }
        private List<Group> Groups { get; } = new List<Group>();

        public Group AddGroup(Group group)
        {
            Groups.Add(group);
            return group;
        }

        public Group AddGroup(string groupName)
        {
            var name = new GroupName(groupName);
            return AddGroup(new Group(name));
        }

        public Student AddStudentToGroup(Student student, Group group)
        {
            if (NumberOfStudents(group)) throw new TooManyStudentsInGroupException();
            group.Students.Add(student);
            return student;
        }

        public Student AddStudent(Group group, string name)
        {
            if (NumberOfStudents(group)) throw new TooManyStudentsInGroupException();
            var student = new Student(name, group, LastId++);
            return AddStudentToGroup(student, group);
        }

        public bool NumberOfStudents(Group group)
        {
            return group.Students.Count + 1 > 5;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in Groups)
            {
                foreach (Student student in group.Students)
                {
                    if (student.Id == id)
                        return student;
                }
            }

            throw new StudentDoesNotExistException();
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in Groups)
            {
                foreach (Student student in group.Students)
                {
                    if (student.Name == name)
                        return student;
                }
            }

            throw new StudentDoesNotExistException();
        }

        public Student FindStudentInGroup(string name, Group group)
        {
            foreach (Student student in group.Students)
            {
                if (student.Name == name) return student;
            }

            throw new StudentIsNotInGroupException();
        }

        public List<Student> FindStudents(string groupName)
        {
            var name = new GroupName(groupName);
            foreach (Group group in Groups)
            {
                if (group.GroupName == name)
                    return group.Students;
            }

            throw new GroupDoesNotExistException();
        }

        public Group FindGroup(string groupName)
        {
            var name = new GroupName(groupName);
            foreach (Group group in Groups)
            {
                if (group.GroupName == name)
                    return group;
            }

            throw new GroupDoesNotExistException();
        }

        public List<Student> FindStudents(char courseNumber)
        {
            var number = new CourseNumber(courseNumber);
            foreach (Group group in Groups)
            {
                if (group.CourseNumber == number)
                    return group.Students;
            }

            throw new CourseDoesNotExistException();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = new List<Group>();
            foreach (Group group in Groups)
            {
                if (group.CourseNumber == courseNumber)
                    groups.Add(group);
            }

            if (groups.Count > 0) return groups;
            throw new CourseDoesNotExistException();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group oldGroup = student.Group;
            oldGroup.Students.Remove(student);
            student.Group = newGroup;
            AddStudentToGroup(student, newGroup);
        }
    }
}