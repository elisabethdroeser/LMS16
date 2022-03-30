using LMS16.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS16.Services
{
    public class CourseSelectListService : ICourseSelectListService
    {
        private readonly ApplicationDbContext db;
        
        public CourseSelectListService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<SelectListItem>> GetCoursesListAsync()
        {
            return await db.Course
                            .Select(c => new SelectListItem
                            {
                                Text = c.Name.ToString(),
                                Value = c.Id.ToString()
                            })
                            .ToListAsync();
                
        }

    }
}
