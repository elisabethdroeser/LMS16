using LMS16.Core.Repositories;
using LMS16.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;

        public ICourseRepository CourseRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.db = context;
            CourseRepository = new CourseRepository(context);
        }
    }
}
