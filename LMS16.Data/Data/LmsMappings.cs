using AutoMapper;
using LMS16.Core.Entities;
using LMS16.Core.ViewModels.CourseViewModels;
using LMS16.Core.ViewModels.UserViewModels;

namespace LMS16.Data.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Course, CourseIndexViewModel>().ReverseMap();

            CreateMap<User, UserIndexViewModel>().ReverseMap();

        }
    }
}
