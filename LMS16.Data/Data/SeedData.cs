using Bogus;
using LMS16.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS16.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<User> userManager = default!;
        private static IEnumerable<Course> courses;

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services, string teacherPW)
        {
            faker = new Faker();

            if (string.IsNullOrWhiteSpace(teacherPW)) throw new Exception("Can´t get password from config");
            //if (string.IsNullOrWhiteSpace(studentPW)) throw new Exception("Can´t get password from config");

            if (context is null) throw new Exception(nameof(ApplicationDbContext));
            
            db = context;

            if (db.Users.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<User>>();
            if (userManager == null) throw new NullReferenceException(nameof(UserManager<User>));
    
            var roleNames = new[] { "Student", "Teacher" };
            var teacherEmail = "teacher@school.se";
            //var studentMail = "student1@school.se";

            var activityTypes = GetActivityTypes();
            await db.AddRangeAsync(activityTypes);
     
            courses = GetCourses();
            await db.AddRangeAsync(courses);

            var modules = GetModules(courses);
            await db.AddRangeAsync(modules);

            var activities = GetActivity(activityTypes, modules);
            await db.AddRangeAsync(activities);

            await AddRolesAsync(roleNames);

            var teacher = await AddTeacherAsync(teacherEmail, teacherPW);
            await AddRolesAsync(teacher, roleNames);

            /*var student = await AddStudentAsync(studentMail, studentPW);
            await AddRolesAsync(student, roleNames);
            */
            await db.SaveChangesAsync();
        }

    
        private static async Task AddRolesAsync(User teacher, string[] roleNames)
        {
            if (teacher == null) throw new NullReferenceException(nameof (teacher));
            foreach (var role in roleNames)
            {
                if (await userManager.IsInRoleAsync(teacher, role)) continue;
                var result = await userManager.AddToRoleAsync(teacher, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
        private static IEnumerable<Activity> GetActivity(IEnumerable<ActivityType> activityTypes, IEnumerable<LMS16.Core.Entities.Module> modules)
        {
            var activities = new List<Activity>();

            foreach (var activityType in activityTypes)
            {
                foreach (var module in modules)
                {
                    if (faker.Random.Int(0, 5) == 0)
                    {
                        var activity = new Activity
                        {
                            Name = faker.Commerce.Product(),
                            Description = faker.Commerce.Department(),
                            StartDate = DateTime.Now.AddDays(faker.Random.Int(-10, 10)),
                            EndDate = faker.Date.Soon(3),
                            ActivityType = activityType,
                            Module = module
                        };
                       activities.Add(activity);
                    }               
                }
            }
            return activities;
        }

        private static IEnumerable<LMS16.Core.Entities.Module> GetModules(IEnumerable<Course> courses)
        {
            var modules = new List<LMS16.Core.Entities.Module>();

            foreach (var course in courses)
            {
                if (faker.Random.Int(0,5) == 0)
                {
                    var module = new LMS16.Core.Entities.Module
                    {
                        Name = faker.Commerce.ProductMaterial(),
                        Description = faker.Commerce.ProductDescription(),
                        StartDate = DateTime.Now.AddDays(faker.Random.Int(-10, 10)),
                        EndDate = faker.Date.Soon(3),
                        Course = course
                    };
                    modules.Add(module);
                } 
            }
            return modules;
        }

        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();

            for (int i = 0; i < 20; i++)
            {
                var course = new Course
                {
                    Name = faker.Commerce.ProductMaterial(),
                    Description = faker.Commerce.ProductDescription(),
                    StartDate = faker.Date.Soon(1),
                };
                courses.Add(course);
            }

            return courses;
        }

        private static List<ActivityType> GetActivityTypes()
        {
            var activityTypes = new List<ActivityType>()
            {
                 new ActivityType(){ Name = "E-learning"},
                 new ActivityType(){ Name = "Lecture"},
                 new ActivityType(){ Name = "Assignment"},
                 new ActivityType(){ Name = "Practice Opportunity"},
                 new ActivityType(){ Name = "Other"}
            };
            return activityTypes;
        }

        private static async Task<User> AddTeacherAsync(string teacherEmail, string teacherPW)
        {
            var found = await userManager.FindByEmailAsync(teacherEmail);

            if (found != null) return null!;

            var teacher = new User
            {
                UserName = teacherEmail,
                Email = teacherEmail,
                FirstName = "Teacher",
                Course = courses.First()
                
            };

            var result = await userManager.CreateAsync(teacher, teacherPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return teacher;
        }
        /*
        private static async Task<User> AddStudentAsync(string studentEmail, string studentPW)
        {
            var found = await userManager.FindByEmailAsync(studentEmail);

            if (found != null) return null!;

            var student = new User
            {
                UserName = studentEmail,
                Email = studentEmail,
                FirstName = "Teacher",

            };

            var result = await userManager.CreateAsync(student, studentPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return student;
        }
        */

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
    }
}
   