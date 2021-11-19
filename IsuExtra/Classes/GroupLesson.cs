namespace IsuExtra.Classes
{
    public class GroupLesson
    {
        public GroupLesson(string discipline, LessonTime lessonTime)
        {
            Discipline = discipline;
            Time = lessonTime;
        }

        public string Discipline { get; }
        public LessonTime Time { get; }
    }
}