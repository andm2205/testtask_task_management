using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public string GetResourceString(string name)
        {
            return _localizer[name];
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateTaskView()
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "CreateTask", null )});
        }
        [HttpGet]
        public IActionResult TaskListView()
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "TaskList", _context.Tasks.ToList()) });
        }
        [HttpPost]
        public IActionResult TaskDesc(AppTask appTask)
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "TaskDesc", appTask) });
        }
        [HttpPost]
        public void CreateTask([Bind("Name, Description, Performers, ScheduledExecutionTime")] AppTask appTask)
        {
            appTask.RegistrationDate = DateTime.Now;
            appTask.Status = Status.Assigned;
            _context.Add(appTask);
            _context.SaveChanges();
        }
        [HttpPost]
        public void UpdateTask(AppTask appTask)
        {
            _context.Update(appTask);
            _context.SaveChanges();
            Console.WriteLine("task updated");
        }
        [HttpPost]
        public void DeleteTask(AppTask appTask)
        {
            _context.Remove(appTask);
            _context.SaveChanges();
            Console.WriteLine("task deleted");
        }
    }
}