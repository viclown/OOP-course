using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class Flow
    {
        private const int MaxNumberOfStudentsInFlow = 5;

        public Flow(List<GroupLesson> groupLessons)
        {
            GroupLessons = groupLessons;
            StudentsExtra = new List<StudentExtra>();
        }

        public List<GroupLesson> GroupLessons { get; set; }
        public List<StudentExtra> StudentsExtra { get; set; }

        public void AddStudentToFlow(StudentExtra student)
        {
            CheckFreePlacesInFlow();
            StudentsExtra.Add(student);
        }

        public void RemoveStudentFromFlow(StudentExtra student)
        {
            StudentsExtra.Remove(student);
        }

        public void CheckFreePlacesInFlow()
        {
            if (StudentsExtra.Count >= MaxNumberOfStudentsInFlow)
                throw new NoFreePlacesInThisFlowException();
        }
    }
}