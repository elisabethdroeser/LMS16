namespace LMS16.Core.Entities
#nullable disable
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    }
}
