using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Core.Entities
#nullable disable
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
