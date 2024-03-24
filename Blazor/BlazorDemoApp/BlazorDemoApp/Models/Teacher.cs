namespace BlazorDemoApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly EntryDate { get; set; }

        public Teacher(int id, string name, DateOnly entryDate)
        {
            Id = id;
            Name = name;
            EntryDate = entryDate;
        }
    }
}
