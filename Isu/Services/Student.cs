namespace Isu.Services
{
    public class Student
    {
        private readonly int _lastId = 100000;
        public Student(string name, Group group)
        {
            Name = name;
            Group = group;
            Id = _lastId++;
        }

        public string Name { get; }
        public Group Group { get; set; }
        public int Id { get; }
    }
}