using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult AppTask()
        {
            return PartialView("AppTask");
        }
        public string GetResourceString(string name)
        {
            return _localizer[name];
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateTaskView()
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "CreateTask", null) });
        }
        [HttpPost]
        public IActionResult CreateTaskView(AppTask parentTask)
        {
            return Json(new
            {
                html = Helper.RenderRazorViewToString(this, "CreateTask",
                    new AppTask
                    {
                        Parent = parentTask,
                        ParentId = parentTask.Id
                    })
            });
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
        public void CreateTask([Bind("Name, Description, Performers, ScheduledExecutionTime, ParentId, Parent")] AppTask appTask)
        {
            appTask.RegistrationDate = DateTime.Now;
            appTask.Status = Status.Assigned;
            _context.Tasks.Add(appTask);
            _context.SaveChanges();
        }
        [HttpPost]
        public void UpdateTask(AppTask appTask)
        {
            _context.Tasks.Update(appTask);
            _context.SaveChanges();
        }
        [HttpPost]
        public void DeleteTask(AppTask rootAppTask)
        {
            if (rootAppTask == null)
                return;
            rootAppTask = _context.Tasks.Single(a => a.Id == rootAppTask.Id);
            List <AppTask> appTasks = new List<AppTask>();
            appTasks.Add(rootAppTask);
            for (int a = 0; a < appTasks.Count; ++a)
            {
                appTasks.AddRange(_context.Tasks.Where(b => b.ParentId == appTasks[a].Id));
            }
            for(int a = appTasks.Count - 1; a >= 0; --a)
            {
                _context.Tasks.Remove(appTasks[a]);
            }
            _context.SaveChanges();
        }
        [HttpPost]
        public IActionResult TaskTree(AppTask appTask)
        {
            var tasks = _context.Tasks.ToList();
            appTask = tasks.Single(a => a.Id == appTask.Id);
            List<string> names = new List<string>();
            while (appTask != null)
            {
                names.Add(appTask.Name);
                appTask = appTask.Parent;
            }
            names.Reverse();
            return Json(new { html = Helper.RenderRazorViewToString(this, "TaskTree", string.Join(" > ", names)) });
        }
    }
}