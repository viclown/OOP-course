using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class OgnpCourse
    {
        public OgnpCourse(char ognpSymbol, List<Flow> flows, List<StudentExtra> students)
        {
            OgnpSymbol = ognpSymbol;
            Flows = flows;
            Students = students;
        }

        public OgnpCourse(char ognpSymbol)
            : this(ognpSymbol, new List<Flow>(), new List<StudentExtra>()) { }

        public char OgnpSymbol { get; set; }
        public List<Flow> Flows { get; set; }
        public List<StudentExtra> Students { get; set; }

        public Flow AddNewFlow(List<GroupLesson> groupLessons)
        {
            var flow = new Flow(groupLessons);
            Flows.Add(flow);
            return flow;
        }

        public List<StudentExtra> GetStudentsFromFlow(Flow flow)
        {
            return flow.StudentsExtra;
        }

        public Flow FindFreeOgnpCourse(StudentExtra student)
        {
            bool flag = true;
            if (Flows.Count == 0)
                throw new NoPossibleFlowsForThisOgnpException();

            foreach (Flow flow in Flows)
            {
                foreach (GroupLesson ognpLesson in flow.GroupLessons)
                {
                    foreach (GroupLesson studentLessons in student.TimeTable)
                    {
                        if (ognpLesson.Time.Equals(studentLessons.Time))
                            flag = false;
                    }
                }

                if (flag)
                {
                    flow.CheckFreePlacesInFlow();
                    return flow;
                }
            }

            throw new NoPossibleFlowsForThisOgnpException();
        }

        public Flow AddStudentToOgnpCourse(StudentExtra student)
        {
            if (student.MegaFaculty == OgnpSymbol)
                throw new ImpossibleToRegisterStudentToThisOgnpBecauseOfMegafacultyException();
            Flow flow = FindFreeOgnpCourse(student);
            flow.AddStudentToFlow(student);
            Students.Add(student);
            student.TimeTable.AddRange(flow.GroupLessons);
            return flow;
        }

        public Flow FindStudentsFlowInOgnpCourse(StudentExtra student)
        {
            foreach (Flow flow in Flows.Where(flow => Enumerable.Contains(flow.StudentsExtra, student)))
            {
                return flow;
            }

            throw new ThisStudentDoesNotTakeThisOgnpCourseException();
        }

        public void RemoveStudentFromOgnpCourse(StudentExtra student)
        {
            Flow studentsFlow = FindStudentsFlowInOgnpCourse(student);

            foreach (GroupLesson lesson in studentsFlow.GroupLessons)
            {
                student.TimeTable.Remove(lesson);
            }

            studentsFlow.RemoveStudentFromFlow(student);
            Students.Remove(student);
        }
    }
}