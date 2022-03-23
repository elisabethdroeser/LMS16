using LMS16.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace LMS16.Core.ViewModels.UserViewModels
{
    public class StudentIndexViewModel
    {
        public string Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
