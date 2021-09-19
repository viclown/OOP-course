namespace Isu.Services
{
    public class Student
    {
        public Student(string name, Group group, int id)
        {
            Name = name;
            Group = group;
            Id = id;
        }

        public string Name { get; set; }
        public Group Group { get; set; }
        public int Id { get; set; }
    }
}