using Microsoft.AspNetCore.Mvc;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentPortal.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbCopntext dbContext;

        public StudentController(ApplicationDbCopntext dbCopntext)
        {
            this.dbContext = dbCopntext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribe = viewModel.Subscribe
            };

            await dbContext.AddAsync(student);

            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
            //_Layout.cshtml => action name "List"; быстрый доступ в navigation bar
        }

        [HttpGet]
        public async Task<IActionResult> Edit (Guid id)
        {
          var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost] 
        public async Task<IActionResult> Edit (Student viewModel)
        {
           var student = await dbContext.Students.FindAsync (viewModel.Id);

            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribe = viewModel.Subscribe;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = dbContext.Students
                .AsNoTracking() // чтобы энтити фреймворк не отслеживал, можно удалить студента
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null)
            {
                dbContext.Students.Remove(viewModel);

                await dbContext.SaveChangesAsync();     
            }

            return RedirectToAction("List", "Student");
        }
    }
}
