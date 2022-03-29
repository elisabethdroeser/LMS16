using LMS16.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace LMS16.Core.ViewModels.StudentViewModels
{
    public class StudentCourseViewModel
    {
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public Course Course { get; set; }
        //public int CourseId { get; set; }

        //public IEnumerable<Activity> Activities { get; set; }
        //public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public IEnumerable<Module> Modules { get; set; } 
        public IEnumerable<User> Attendees { get; set; }
    }
}
