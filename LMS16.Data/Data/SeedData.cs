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

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services, string teacherPW, string studentPW)
        {
            faker = new Faker();

            if (string.IsNullOrWhiteSpace(teacherPW)) throw new Exception("Can´t get password from config");
            if (string.IsNullOrWhiteSpace(studentPW)) throw new Exception("Can´t get password from config");

            if (db == null) throw new Exception(nameof(ApplicationDbContext));
            
            db = context;

            if (db.Users.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<User>>();
            if (userManager == null) throw new NullReferenceException(nameof(UserManager<User>));
    
            var roleNames = new[] { "Student", "Teacher" };
            //var teacherMail = "teacher1@school.se";
            //var studentMail = "student1@school.se";

            var activityTypes = GetActivityTypes();
            await db.AddRangeAsync(activityTypes);
            await db.SaveChangesAsync();

            //await AddRolesAsync(rolenNmes);
            //AspNetRoles

            var courses = GetCourses();
            await db.AddRangeAsync(courses);
            await db.SaveChangesAsync();

            //await AddRolesAsync(roleNames);
            //AspNetRoleClaims
            //AspNetUsers

            var modules = GetModules(courses);
            await db.AddRangeAsync(modules);
            await db.SaveChangesAsync();
            //await AddRolesAsync(roleNames);

            //AspNetUserClaims
            //AspNetUserLogins
            //AspNetUserRoles
            //AspNetUserTokens

            var activities = GetActivity(activityTypes, modules);
            await db.AddRangeAsync(activities);
            await db.SaveChangesAsync();

            //await AddRolesAsync(roleNames);

            //var teacher = await AddTeacherAsync(teacherMail, teacherPW);
            //await AddRolesAsync(teacher, roleNames);

            //var student = await AddStudentAsync(studentMail, studentPW);
            //await AddRolesAsync(student, roleNames);

            //var users = GetUsers();
            //await db.AddRangeAsync(users);  

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
                            Description = faker.Commerce.ProductDescription.Department(),
                            StartDate = faker.Date.Soon(1),
                            EndDate = faker.Date.Past(1),
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
                        StartDate = faker.Date.Soon(1),
                        EndDate = faker.Date.Past(2)
                        Course = course
                    };
                    modules.Add(module);
                } 
            }
            return modules;
        }

        private static IEnumerable<Course> GetCourses()
        {
            
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
    }
}
   