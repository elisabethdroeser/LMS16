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
using Module = LMS16.Core.Entities.Module;

namespace LMS16.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<User> userManager = default!;

        public static async Task InitAsync(ApplicationDbContext db, IServiceProvider services, string teacherPW, string studentPW)
        {
            faker = new Faker();

            if (string.IsNullOrWhiteSpace(teacherPW)) throw new Exception("Can´t get password from config");
            if (string.IsNullOrWhiteSpace(studentPW)) throw new Exception("Can´t get password from config");

            if (db == null) throw new Exception(nameof(ApplicationDbContext));

            if (db.Users.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<User>>();
            if (userManager == null) throw new NullReferenceException(nameof(UserManager<User>));
    
            var roleNames = new[] { "Student", "Teacher" };
            var teacherMail = "teacher1@school.se";
            var studentMail = "student1@school.se";

            var activityTypes = GetActivityType();
            await db.AddRangeAsync(activityTypes);

            //await AddRolesAsync(rolenames);
            //AspNetRoles
            
            var courses = GetCourses();
            await db.AddRangeAsync(courses);

            //await AddRolesAsync(rolenames);
            //AspNetRoleClaims
            //AspNetUsers
            var modules = GetModules(courses);
            await db.AddRangeAsync(modules);
            //await AddRolesAsync(rolenames);

            //AspNetUserClaims
            //AspNetUserLogins
            //AspNetUserRoles
            //AspNetUserTokens

            var activities = GetActivity(activityTypes, modules);
            await db.AddRangeAsync(activities);

            await AddRolesAsync(rolenames);

            var teacher = await AddTeacherAsync(teacherMail, teacherPW);
            await AddRolesAsync(teacher, roleNames);

            var student = await AddStudentAsync(studentMail, studentPW);
            await AddRolesAsync(student, roleNames);

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

        private static IEnumerable<Activity> GetActivity(List<ActivityType> activityTypes, List<Module> modules)
        {
            throw new NotImplementedException();
        }

        private static object GetModules(object courses)
        {
            throw new NotImplementedException();
        }

        private static object GetCourses()
        {
            throw new NotImplementedException();
        }

        private static object GetActivityType()
        {
            throw new NotImplementedException();
        }






    }

}
   