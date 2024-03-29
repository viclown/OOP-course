﻿using System;
using System.Collections.Generic;
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
            StudentExtra denis = _isuService.AddStudentExtra("Novikov Denis", m3204);
            Assert.AreEqual(m3204, denis.Group);
            Assert.AreEqual(_isuService.FindStudentExtra("Novikov Denis"), denis);
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
            var mon1 = new LessonTime(DayOfWeek.Monday, LessonNumber.FirstLesson);
            var mon2 = new LessonTime(DayOfWeek.Monday, LessonNumber.SecondLesson);
            var tue1 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FirstLesson);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.SecondLesson);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.ThirdLesson);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FourthLesson);
            
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
            fotonics.AddStudentToOgnpCourse(danya, flow1);

            Assert.Contains(danya, fotonics.Students);
        }
        
        [Test]
        public void RegisterStudentToOgnpAndRemoveHim()
        {
            var mon1 = new LessonTime(DayOfWeek.Monday, LessonNumber.FirstLesson);
            var mon2 = new LessonTime(DayOfWeek.Monday, LessonNumber.SecondLesson);
            var tue1 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FirstLesson);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.SecondLesson);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.ThirdLesson);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FourthLesson);
            
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
            fotonics.AddStudentToOgnpCourse(danya, flow1);

            Assert.Contains(danya, fotonics.Students);
            
            fotonics.RemoveStudentFromOgnpCourse(danya);

            bool danyaIsInOgnp = fotonics.Students.Contains(danya);
            Assert.AreEqual(false, danyaIsInOgnp);
        }
        
        [Test]
        public void GetOgnpFlows()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FirstLesson);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.SecondLesson);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.ThirdLesson);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FourthLesson);
            var wed1 = new LessonTime(DayOfWeek.Wednesday, LessonNumber.FirstLesson);
            var wed2 = new LessonTime(DayOfWeek.Wednesday, LessonNumber.SecondLesson);
            var wed3 = new LessonTime(DayOfWeek.Wednesday, LessonNumber.ThirdLesson);
            var wed4 = new LessonTime(DayOfWeek.Wednesday, LessonNumber.FourthLesson);
            
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
            Assert.AreEqual(fotonics.Flows, flows);
        }
        
        [Test]
        public void GetStudentFromAnOgnpFlow()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FirstLesson);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.SecondLesson);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.ThirdLesson);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FourthLesson);
            
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
            
            fotonics.AddStudentToOgnpCourse(masha, flow1);
            fotonics.AddStudentToOgnpCourse(danya, flow1);
            fotonics.AddStudentToOgnpCourse(vika, flow1);

            List<StudentExtra> studentsOgnpFlow = fotonics.GetStudentsFromFlow(flow1);
            Assert.AreEqual(studentsOgnpFlow.Count, 3);
            Assert.Contains(danya, studentsOgnpFlow);
            Assert.Contains(vika, studentsOgnpFlow);
            Assert.Contains(masha, studentsOgnpFlow);
        }

        [Test]
        public void GetStudentWithNoOgnp()
        {
            var tue1 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FirstLesson);
            var tue2 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.SecondLesson);
            var tue3 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.ThirdLesson);
            var tue4 = new LessonTime(DayOfWeek.Tuesday, LessonNumber.FourthLesson);

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

            fotonics.AddStudentToOgnpCourse(masha, flow1);

            List<StudentExtra> studentsWithNoOgnp = _isuService.GetStudentsWithNoOgnp(m3204);
            Assert.AreEqual(studentsWithNoOgnp.Count, 2);
            Assert.Contains(danya, studentsWithNoOgnp);
            Assert.Contains(vika, studentsWithNoOgnp);
            bool mashaInList = studentsWithNoOgnp.Contains(masha);
            Assert.AreEqual(false, mashaInList);
        }
    }
}