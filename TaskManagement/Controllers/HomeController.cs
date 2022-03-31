using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateTask()
        {
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "CreateTask", null )});
        }
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Tasks.ToList()) });
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([Bind("Name, Description, Performers, ScheduledExecutionTime")] AppTask appTask)
        {
            appTask.RegistrationDate = DateTime.Now;
            appTask.Status = Status.Assigned;
            _context.Add(appTask);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Tasks.ToList()) });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask(AppTask appTask)
        {
            _context.Update(appTask);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Tasks.ToList()) });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(AppTask appTask)
        {
            _context.Remove(appTask);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Tasks.ToList()) });
        }
    }
}