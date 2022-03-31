using LMS16.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace LMS16.Core.Dto
{
    public class ModuleDto
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
