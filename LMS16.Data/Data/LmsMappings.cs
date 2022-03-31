using AutoMapper;
using LMS16.Core.Dto;
using LMS16.Core.Entities;
using LMS16.Core.ViewModels.CourseViewModels;
using LMS16.Core.ViewModels.StudentViewModels;
using LMS16.Core.ViewModels.UserViewModels;

namespace LMS16.Data.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Course, CourseIndexViewModel>().ReverseMap();
            CreateMap<Course, CourseEditViewModel>().ReverseMap();
            CreateMap<Course, CourseDetailsViewModel>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<Course, StudentCourseViewModel>().ReverseMap();

            CreateMap<User, UserIndexViewModel>().ReverseMap();
            

        }
    }
}
