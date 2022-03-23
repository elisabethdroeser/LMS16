using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LMS16.Core.Entities
#nullable disable
{
    public class Course
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "A short description of the course")]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<User> AttendingStudents { get; set; } = new List<User>();

        public Course()
        {
        }

        public Course(string name, string description, DateTime startdate)
        {
            Name = name;
            Description = description;
            StartDate = startdate;  
        }

        private class CourseNameAttribute : Attribute
        {
        }
    }
}
