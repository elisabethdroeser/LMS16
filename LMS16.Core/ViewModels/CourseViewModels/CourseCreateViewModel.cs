using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace LMS16.Core.ViewModels.CourseViewModels
{
    public class CourseCreateViewModel
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "A short description of the course")]
        //[CourseName(6)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
    }
}
