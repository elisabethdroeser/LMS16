using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LMS16.Core.Entities
#nullable disable
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
