using AutoMapper;
using LMS16.Core.Entities;
using LMS16.Core.ViewModels.CourseViewModels;
using LMS16.Core.ViewModels.StudentViewModels;
using LMS16.Core.ViewModels.UserViewModels;
using LMS16.Data.Data;
using LMS16.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace LMS16.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly CourseRepository courseRepo;

        public CoursesController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            db = context;
            this.userManager = userManager;
            this.mapper = mapper;
            courseRepo = new CourseRepository(db);
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction(nameof(IndexTeacher));
            }
            else if (User.IsInRole("Student"))
            {
                return RedirectToAction(nameof(IndexStudent));
            }

         
            var viewModel = mapper.ProjectTo<CourseIndexViewModel>(db.Course)
                                    .OrderByDescending(c => c.Id)
                                    .Take(5);

            return View(await viewModel.ToListAsync());
            
        }

/*
        public async Task<IActionResult> IndexAttending(int? id)
        {
            if (id == null) return BadRequest();

            var userId = userManager.GetUserId(User);
            /*
            var course = db.Course.Include(s => s.AttendingStudents)
                                    .FirstOrDefault(a => a.Id == id);

            var attending = course?.AttendingStudents
                                    .FirstOrDefault(a => a.Id == userId);
            
            var attending = db.Course.FindAsync(userId, id);

            if (attending == null)
            {
                var booking = new Course
                {
                    Id = (int)userId

                };
                db.Course.Add(booking);
             }
            else
            {
                db.Remove(attending);
            }
            await db.SaveChangesAsync(); 

            return RedirectToAction(Index)
        }
  

        /return View(await viewModel.ToListAsync());
        return View(await db.Course.ToListAsync());
        //new { id = userManager.Get...(User)}
        */



        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> IndexTeacher()
        {
            return await courseRepo.GetAsync();
            //var viewModel = mapper.ProjectTo<CourseIndexViewModel>(db.Course);


            //mapper.ProjectTo<CourseIndexViewModel>(db.Course);

            /*
            var viewModel = new CourseIndexViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description
                //Modules = course.Modules,

                //AttendingStudents = course.AttendingStudents
            };
                */
            return View(viewModel);
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> IndexStudent()
        {
            var userId = userManager.GetUserId(User);
     
            var user = db.Users.Find(userId);

            var course = await db.Course
                .Include(c => c.AttendingStudents)
                .Include(c => c.Modules)
                .ThenInclude(m => m.Activities)
                .ThenInclude(a => a.ActivityType)
                .FirstOrDefaultAsync(c => c.Id == user.CourseId);

            var viewModel = new StudentCourseViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Course = course,              
                Modules = course.Modules,
                Attendees = course.AttendingStudents
            };

            //var name = viewModel.Modules.First().Activities.ToList()[0].ActivityType.Name;

            //return View(nameof(IndexStudent), viewModel);
            return View(viewModel);
        }
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var course = await db.Course
            //   .FirstOrDefaultAsync(c => c.Id == id);

            var course = await mapper.ProjectTo<CourseDetailsViewModel>(db.Course)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate
            };

            if (course == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Course.FirstOrDefaultAsync(c => c.Id == id);


            var viewModel = new CourseEditViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate
            };

            if (course == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var course = await db.Course.FirstOrDefaultAsync(c => c.Id == id);

                    mapper.Map(viewModel, course);

                    db.Update(course);

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await db.Course.FindAsync(id);
            db.Course.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return db.Course.Any(e => e.Id == id);
        }
    }
}