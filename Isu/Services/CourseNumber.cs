using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        public CourseNumber(char courseNumber)
        {
            if (!IsNumberAllowed(courseNumber)) throw new InvalidCourseNumberException();
            Number = courseNumber;
        }

        private int Number { get; }

        private static bool IsNumberAllowed(char courseNumber)
        {
            return courseNumber >= '1' && courseNumber <= '4';
        }
    }
}