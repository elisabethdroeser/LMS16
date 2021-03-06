namespace LMS16.Core.Entities
#nullable disable
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Module Module { get; set; }
        public int ModuleId { get; set; }

        public ActivityType ActivityType { get; set; }
        public int ActivityTypeId { get; set; }

        public Activity()
        {
        }

        public Activity (string name, string description, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;   
        }

    }
}
