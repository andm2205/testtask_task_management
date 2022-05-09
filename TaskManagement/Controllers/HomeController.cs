using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;
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
        public IActionResult AppTask()
        {
            return PartialView("AppTask");
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
            try
            {
                return Json(new { html = Helper.RenderRazorViewToString(this, "TaskList", _context.Tasks.ToList()) });
            }
            catch (Exception exception)
            {
                return Json(new {error = true, message = exception.Message});
            }
        }
        [HttpGet]
        public IActionResult TaskDesc()
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "TaskDesc", null), message = _localizer["Message1"].Value });
        }
        [HttpPost]
        public IActionResult TaskDesc(AppTask appTask)
        {
            return Json(new { html = Helper.RenderRazorViewToString(this, "TaskDesc", appTask), message = _localizer["Message1"].Value });
        }
        [HttpPost]
        public IActionResult CreateTask([Bind("Name, Description, Performers, ScheduledExecutionTime, ParentId, Parent")] AppTask appTask)
        {
            try
            {
                appTask.RegistrationDate = DateTime.Now;
                appTask.Status = Status.Assigned;
                
                if(appTask.ParentId != null)
                {
                    var ancestors = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetParents(@id)", new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = appTask.ParentId,
                        SqlDbType = SqlDbType.BigInt
                    });
                    foreach (var a in ancestors)
                        a.ScheduledExecutionTime += appTask.ScheduledExecutionTime;
                    _context.Tasks.UpdateRange(ancestors);
                }
                _context.Tasks.Add(appTask);
                _context.SaveChanges();

                return Json(new { message = _localizer["Message1"].Value });
            }
            catch (Exception exception)
            {
                return Json(new { message = exception.Message, error = true });
            }
        }
        [HttpPost]
        public IActionResult UpdateTask(AppTask appTask)
        {
            try
            {
                AppTask oldAppTask = _context.Tasks.AsNoTracking().Single(a => a.Id == appTask.Id);
                if (appTask.Status != oldAppTask.Status
                    && appTask.Status == Status.Completed && oldAppTask.Status != Status.InProgress)
                    throw new Exception(_localizer["Message2"].Value);
                if (appTask.Status != oldAppTask.Status
                    && appTask.Status == Status.Suspended && oldAppTask.Status != Status.InProgress)
                    throw new Exception(_localizer["Message3"].Value);
                if (appTask.Status == Status.Completed && oldAppTask.Status != Status.Completed)
                {
                    var children = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetTree(@id)", new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = appTask.Id,
                        SqlDbType = SqlDbType.BigInt
                    });
                    foreach (var a in children)
                        if (a.Status != Status.InProgress && a.Status != Status.Completed)
                            throw new Exception(_localizer["Message2"].Value);
                        else
                            a.Status = Status.Completed;
                    _context.Tasks.UpdateRange(children);
                }
                _context.Tasks.Update(appTask);
                if(appTask.ParentId != null)
                {
                    if (appTask.ActualExecutionTime != oldAppTask.ActualExecutionTime)
                    {
                        var ancestors = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetParents(@id)", new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.ParentId,
                            SqlDbType = SqlDbType.BigInt
                        });
                        foreach (var a in ancestors)
                            a.ActualExecutionTime += appTask.ActualExecutionTime - oldAppTask.ActualExecutionTime;
                        _context.Tasks.UpdateRange(ancestors);
                    }
                    if (appTask.ScheduledExecutionTime != oldAppTask.ScheduledExecutionTime)
                    {
                        var ancestors = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetParents(@id)", new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.ParentId,
                            SqlDbType = SqlDbType.BigInt
                        });
                        foreach (var a in ancestors)
                            a.ScheduledExecutionTime += appTask.ScheduledExecutionTime - oldAppTask.ScheduledExecutionTime;
                        _context.Tasks.UpdateRange(ancestors);
                    }
                }
                _context.SaveChanges();
                return Json(new { message = _localizer["Message1"].Value });
            }
            catch(Exception exception)
            {
                return Json(new { message = exception.Message, error = true });
            }
        }
        [HttpPost]
        public IActionResult DeleteTask(AppTask appTask)
        {
            try
            {
                appTask = _context.Tasks.AsNoTracking().Single(a => a.Id == appTask.Id);

                if(appTask.ParentId != null)
                {
                    if (appTask.ActualExecutionTime > 0)
                    {
                        var ancestors = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetParents(@id)", new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.ParentId,
                            SqlDbType = SqlDbType.BigInt
                        });
                        foreach (var a in ancestors)
                            a.ActualExecutionTime -= appTask.ActualExecutionTime;
                        _context.Tasks.UpdateRange(ancestors);
                    }

                    if (appTask.ScheduledExecutionTime > 0)
                    {
                        var ancestors = _context.Tasks.FromSqlRaw(@"SELECT * FROM GetParents(@id)", new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.ParentId,
                            SqlDbType = SqlDbType.BigInt
                        });
                        foreach (var a in ancestors)
                            a.ScheduledExecutionTime -= appTask.ScheduledExecutionTime;
                        _context.Tasks.UpdateRange(ancestors);
                    }
                }

                _context.Tasks.RemoveRange(
                        _context.Tasks.FromSqlRaw(
                        "SELECT * FROM GetTree(@id);",
                        new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.Id,
                            SqlDbType = SqlDbType.BigInt
                        }));

                _context.SaveChanges();

                return Json(new { message = _localizer["Message1"].Value });
            }
            catch (Exception exception)
            {
                return Json(new { message = exception.Message, error = true });
            }
        }
        [HttpPost]
        public IActionResult TaskTree(AppTask appTask)
        {
            try
            {
                return Json(new
                {
                    html = Helper.RenderRazorViewToString(this, "TaskTree", string.Join(" > ", _context.Tasks.FromSqlRaw(
                        "SELECT * FROM GetParents(@id)",
                        new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = appTask.Id,
                            SqlDbType = SqlDbType.BigInt
                        }).Select(a => a.Name).ToArray().Reverse())),
                    message = _localizer["Message1"].Value
                });
            }
            catch(Exception exception)
            {
                return Json(new { message = exception.Message, error = true });
            }
        }
    }
}