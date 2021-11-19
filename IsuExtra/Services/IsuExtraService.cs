using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Classes;

namespace IsuExtra.Services
{
    public class IsuExtraService : IsuService
    {
        private List<GroupExtra> Groups { get; } = new List<GroupExtra>();
        private List<OgnpCourse> OgnpCourses { get; } = new List<OgnpCourse>();

        public GroupExtra AddGroupExtra(string groupName)
        {
            var name = new GroupName(groupName);
            return AddGroupExtra(new GroupExtra(name));
        }

        public OgnpCourse AddOgnpCourse(OgnpCourse ognpCourse)
        {
            OgnpCourses.Add(ognpCourse);
            return ognpCourse;
        }

        public OgnpCourse FindOgnpCourse(char ognpSymbol)
        {
            return OgnpCourses.Find(ognpCourse => ognpCourse.OgnpSymbol.Equals(ognpSymbol));
        }

        public GroupExtra FindGroupExtra(string groupName)
        {
            var name = new GroupName(groupName);
            return Groups.Find(group => group.GroupName.Equals(name));
        }

        public StudentExtra FindStudentExtra(string name)
        {
            return Groups.SelectMany(@group => @group.StudentsExtra)
                .FirstOrDefault(student => student.Name == name);
        }

        public void AddLessonToGroupTimetable(GroupExtra groupExtra, GroupLesson groupLesson)
        {
            groupExtra.AddDisciplineToTimeTable(groupLesson);
        }

        public List<StudentExtra> GetOgnpStudents(OgnpCourse ognp)
        {
            return ognp.Students;
        }

        public List<Flow> GetOgnpFlows(OgnpCourse ognp)
        {
            return ognp.Flows;
        }

        public StudentExtra AddStudentExtra(string name, GroupExtra groupExtra)
        {
            if (groupExtra.CheckIfGroupHasFreePlaces()) throw new TooManyStudentsInGroupException();
            var student = new StudentExtra(name, groupExtra);
            return groupExtra.AddStudent(student);
        }

        public List<StudentExtra> GetStudentsWithNoOgnp(GroupExtra group)
        {
            var studentsWithoutOgnp = new List<StudentExtra>();
            studentsWithoutOgnp.AddRange(group.StudentsExtra);
            foreach (StudentExtra student in OgnpCourses
                .SelectMany(ognpCourse => @group.StudentsExtra
                    .Where(student => ognpCourse.Students.Contains(student))))
            {
                studentsWithoutOgnp.Remove(student);
            }

            return studentsWithoutOgnp;
        }

        private GroupExtra AddGroupExtra(GroupExtra group)
        {
            Groups.Add(group);
            return group;
        }
    }
}