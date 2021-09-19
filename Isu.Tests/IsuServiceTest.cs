using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            var isu = new IsuService();
            Group m3204 = isu.AddGroup("M3204");
            Group m3205 = isu.AddGroup("M3205");
            Group m3405 = isu.AddGroup("M3405");
            Student danil = isu.AddStudent(m3204, "Kazancev Danil");
            Student ilya = isu.AddStudent(m3405, "Shamov Ilya");
            Student vika = isu.AddStudent(m3204, "Zakharova Viktoriia");
            Student leha = isu.AddStudent(m3204, "Ivanov Alexey");
            Student serj = isu.AddStudent(m3204, "Ivanov Sergey");
            _isuService = isu;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3204 = _isuService.FindGroup("M3204");
            Student denis = _isuService.AddStudent(m3204, "Bespalov Denis");
            Assert.IsTrue(denis.Group == m3204);
            Assert.IsTrue(denis == _isuService.FindStudent(denis.Name));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group m3204 = _isuService.FindGroup("M3204");
            Student denis = _isuService.AddStudent(m3204, "Bespalov Denis");
            Assert.Catch<IsuException>(() =>
            {
                Student misha = _isuService.AddStudent(m3204, "Suslov Michail");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group l1205 = _isuService.AddGroup("L1205");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group m3304 = _isuService.AddGroup("M3304");
            Student daniil = _isuService.AddStudent(m3304, "Titov Daniil");
            Group m3404 = _isuService.AddGroup("M3404");
            _isuService.ChangeStudentGroup(daniil, m3404);
            Assert.Catch<IsuException>(() =>
            {
                Student daniil = _isuService.FindStudentInGroup("Titov Daniil", _isuService.FindGroup("M3304"));
            });
        }
    }
}