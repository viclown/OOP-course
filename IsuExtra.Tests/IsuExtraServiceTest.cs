using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Classes;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuExtraService _isuService;

        [SetUp]
        public void Setup()
        {
            var isu = new IsuExtraService();
            GroupExtra m3204 = isu.AddGroupExtra("M3204");
            GroupExtra m3205 = isu.AddGroupExtra("M3205");
            StudentExtra danil = isu.AddStudentExtra("Kazancev Danya", m3204);
            StudentExtra vika = isu.AddStudentExtra("Zakharova Viktoriia", m3204);
            StudentExtra masha = isu.AddStudentExtra("Malysheva Maria", m3204);
            _isuService = isu;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            GroupExtra m3204 = _isuService.FindGroupExtra("M3204");
            StudentExtra denis = _isuService.AddStudentExtra("Bespalov Denis", m3204);
            Assert.IsTrue(denis.Group == m3204);
            Assert.IsTrue(denis == _isuService.FindStudentExtra(denis.Name));
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                GroupExtra l1205 = _isuService.AddGroupExtra("C10Wn");
            });
        }

        [Test]
        public void AddNewOgnpAndRegisterStudentToOgnp()
        {
            var mon1 = new LessonTime(DayOfWeek.Monday, 1);
            var mon2 = new LessonTime(DayOfWeek.Monday, 2);
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);
            
            OgnpCourse fotonics = _isuService.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons);
            
            GroupExtra m3204 = _isuService.FindGroupExtra("M3204");
            
            var physicsMon1 = new GroupLesson("Physics", mon1);
            _isuService.AddLessonToGroupTimetable(m3204, physicsMon1);
            var oopMon2 = new GroupLesson("OOP", mon2);
            _isuService.AddLessonToGroupTimetable(m3204, oopMon2);

            StudentExtra danya = _isuService.FindStudentExtra("Kazancev Danya");
            fotonics.AddStudentToOgnpCourse(danya);

            Assert.IsTrue(fotonics.Students.Contains(danya));
        }
        
        [Test]
        public void RegisterStudentToOgnpAndRemoveHim()
        {
            var mon1 = new LessonTime(DayOfWeek.Monday, 1);
            var mon2 = new LessonTime(DayOfWeek.Monday, 2);
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);
            
            OgnpCourse fotonics = _isuService.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons);
            
            GroupExtra m3204 = _isuService.FindGroupExtra("M3204");
            
            var physicsMon1 = new GroupLesson("Physics", mon1);
            _isuService.AddLessonToGroupTimetable(m3204, physicsMon1);
            var oopMon2 = new GroupLesson("OOP", mon2);
            _isuService.AddLessonToGroupTimetable(m3204, oopMon2);

            StudentExtra danya = _isuService.FindStudentExtra("Kazancev Danya");
            fotonics.AddStudentToOgnpCourse(danya);

            Assert.IsTrue(fotonics.Students.Contains(danya));
            
            fotonics.RemoveStudentFromOgnpCourse(danya);
            
            Assert.IsFalse(fotonics.Students.Contains(danya));
        }
        
        [Test]
        public void GetOgnpFlows()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);
            var wed1 = new LessonTime(DayOfWeek.Wednesday, 1);
            var wed2 = new LessonTime(DayOfWeek.Wednesday, 2);
            var wed3 = new LessonTime(DayOfWeek.Wednesday, 3);
            var wed4 = new LessonTime(DayOfWeek.Wednesday, 4);
            
            OgnpCourse fotonics = _isuService.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons1 = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons1);
            
            var ognpFotonicsLessons2 = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", wed1),
                new GroupLesson("Physical optics", wed2),
                new GroupLesson("Physical optics", wed3),
                new GroupLesson("Laser technologies", wed4),
            };
            Flow flow2 = fotonics.AddNewFlow(ognpFotonicsLessons2);

            List<Flow> flows = _isuService.GetOgnpFlows(fotonics);
            Assert.IsTrue(fotonics.Flows == flows);
        }
        
        [Test]
        public void GetStudentFromAnOgnpFlow()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);
            
            OgnpCourse fotonics = _isuService.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons1 = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons1);
            
            StudentExtra danya = _isuService.FindStudentExtra("Kazancev Danya");
            StudentExtra vika = _isuService.FindStudentExtra("Zakharova Viktoriia");
            StudentExtra masha = _isuService.FindStudentExtra("Malysheva Maria");
            
            fotonics.AddStudentToOgnpCourse(masha);
            fotonics.AddStudentToOgnpCourse(danya);
            fotonics.AddStudentToOgnpCourse(vika);

            List<StudentExtra> studentsOgnpFlow = fotonics.GetStudentsFromFlow(flow1);
            Assert.IsTrue(studentsOgnpFlow.Count == 3);
            Assert.IsTrue(studentsOgnpFlow.Contains(danya));
            Assert.IsTrue(studentsOgnpFlow.Contains(vika));
            Assert.IsTrue(studentsOgnpFlow.Contains(masha));
        }

        [Test]
        public void GetStudentWithNoOgnp()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, 1);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, 2);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, 3);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, 4);

            OgnpCourse fotonics = _isuService.AddOgnpCourse(new OgnpCourse('F'));
            var ognpFotonicsLessons1 = new List<GroupLesson>
            {
                new GroupLesson("Physical optics", tue1),
                new GroupLesson("Physical optics", tue2),
                new GroupLesson("Physical optics", tue3),
                new GroupLesson("Laser technologies", tue4),
            };
            Flow flow1 = fotonics.AddNewFlow(ognpFotonicsLessons1);

            StudentExtra danya = _isuService.FindStudentExtra("Kazancev Danya");
            StudentExtra vika = _isuService.FindStudentExtra("Zakharova Viktoriia");
            StudentExtra masha = _isuService.FindStudentExtra("Malysheva Maria");
            GroupExtra m3204 = _isuService.FindGroupExtra("M3204");

            fotonics.AddStudentToOgnpCourse(masha);

            List<StudentExtra> studentsWithNoOgnp = _isuService.GetStudentsWithNoOgnp(m3204);
            Assert.IsTrue(studentsWithNoOgnp.Count == 2);
            Assert.IsTrue(studentsWithNoOgnp.Contains(danya));
            Assert.IsTrue(studentsWithNoOgnp.Contains(vika));
            Assert.IsFalse(studentsWithNoOgnp.Contains(masha));
        }
    }
}