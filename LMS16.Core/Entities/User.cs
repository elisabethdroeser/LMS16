using System.ComponentModel.DataAnnotations;

namespace LMS16.Core.Entities
#nullable disable
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public Course Course { get; set; }
        public int CourseId { get; set; }

    }
}
