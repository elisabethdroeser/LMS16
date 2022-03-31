using LMS16.Core.Entities;
using LMS16.Core.Repositories;
using LMS16.Core.ViewModels.CourseViewModels;
using LMS16.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext db;
        public CourseRepository(ApplicationDbContext db)
        {
            this.db = db;
                
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await db.Course.ToListAsync();
        }
    }
}
