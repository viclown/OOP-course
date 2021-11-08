using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Classes
{
    public class StudentExtra : Student
    {
        public StudentExtra(string name, GroupExtra @group)
            : base(name, @group)
        {
            var lessons = new List<GroupLesson>();
            lessons.AddRange(group.TimeTable);
            TimeTable = lessons;
            MegaFaculty = group.MegaFaculty;
        }

        public List<GroupLesson> TimeTable { get; set; }
        public char MegaFaculty { get; }
    }
}