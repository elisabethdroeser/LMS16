using LMS16.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace LMS16.Core.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "A short description of the course")]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<User> AttendingStudents { get; set; } = new List<User>();
        public ICollection<ModuleDto> Modules { get; set; } = new List<ModuleDto>();

    }
}
