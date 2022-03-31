using LMS16.Core.Entities;
using LMS16.Data.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Data.Repositories
{
    public class CourseRepository
    {
        private readonly ApplicationDbContext db;
        public CourseRepository(ApplicationDbContext db)
        {
            this.db = db;
                
        }

        public Task<IEnumerable<Course>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
