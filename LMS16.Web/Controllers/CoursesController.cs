using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS16.Core.Entities;
using LMS16.Data.Data;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using LMS16.Core.ViewModels.CourseViewModels;
#nullable disable
namespace LMS16.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CoursesController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            db = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var viewModel = mapper.ProjectTo<CourseIndexViewModel>(db.Course)
                .OrderByDescending(c => c.Id)
                .Take(15);

            return View(await viewModel.ToListAsync());
        }

        public async Task<IActionResult> IndexTeacher(int? id)
        {
            if (id == null) return BadRequest();

            var userId = userManager.GetUserId(User);

            var 

        }

        public async Task<IActionResult> IndexStudent(int? id)
        {
            if (id == null) return BadRequest();

            var userId = userManager.GetUserId(User);

            var

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
