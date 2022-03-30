using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS16.Services
{
    public interface ICourseSelectListService
    {
        Task<IEnumerable<SelectListItem>> GetCoursesListAsync();
    }
}
