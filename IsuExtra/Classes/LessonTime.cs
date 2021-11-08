using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class LessonTime
    {
        public LessonTime(DayOfWeek weekday, int lessonNumber)
        {
            if (!IsLessonNumberCorrect(lessonNumber)) throw new InvalidLessonNumberException();
            Weekday = weekday;
            LessonNumber = lessonNumber;
        }

        public DayOfWeek Weekday { get; }
        public int LessonNumber { get; }

        public bool IsLessonNumberCorrect(int lessonNumber)
        {
            return lessonNumber is >= 0 and <= 7;
        }
    }
}