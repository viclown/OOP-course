using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class LessonTime
    {
        public LessonTime(DayOfWeek weekday, LessonNumber lessonNumber)
        {
            Weekday = weekday;
            LessonNumber = lessonNumber;
        }

        public DayOfWeek Weekday { get; }
        public LessonNumber LessonNumber { get; }
    }
}