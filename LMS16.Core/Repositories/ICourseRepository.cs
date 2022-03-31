using LMS16.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Core.Repositories
{
    public interface ICourseRepository
    {
        Task <IEnumerable<Course>> GetCoursesAsync();
    }
}
