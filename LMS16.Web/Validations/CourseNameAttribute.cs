using LMS16.Core.ViewModels.CourseViewModels;
using System.ComponentModel.DataAnnotations;

namespace LMS16.Validations
{
    public class CourseNameAttribute : ValidationAttribute
    {
       public CourseNameAttribute()
        {
            ErrorMessage = "Course name must be in 3 UPPERCASE LETTERS and 3 digits";
        }

        public override bool IsValid(object? value)
        {
            if (!(value is string s))
                throw new ArgumentException("Not as requested");
            return s == s.ToUpper();
        }
    }
}
