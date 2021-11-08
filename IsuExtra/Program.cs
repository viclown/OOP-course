using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using IsuExtra.Classes;
using IsuExtra.Services;

namespace IsuExtra
{
    internal class Program
    {
        private static void Main()
        {
            var isu = new IsuExtraService();

            var mon1 = new LessonTime(DayOfWeek.Monday, 1);
            var mon2 = new LessonTime(DayOfWeek.Monday, 2);
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);

            OgnpCourse fotonics = isu.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons);

            GroupExtra m3204 = isu.AddGroupExtra("M3204");
            var physicsMon1 = new GroupLesson("Physics", mon1);
            isu.AddLessonToGroupTimetable(m3204, physicsMon1);
            var oopMon2 = new GroupLesson("OOP", mon2);
            isu.AddLessonToGroupTimetable(m3204, oopMon2);

            StudentExtra danya = isu.AddStudentExtra("Danya Kazancev", m3204);
            StudentExtra vika = isu.AddStudentExtra("Viktoriia Zakharova", m3204);
            StudentExtra misha = isu.AddStudentExtra("Misha Suslov", m3204);
            fotonics.AddStudentToOgnpCourse(danya);

            List<StudentExtra> studentsNoOgnp = isu.GetStudentsWithNoOgnp(m3204);
            Console.WriteLine(studentsNoOgnp.Count);
        }
    }
}
