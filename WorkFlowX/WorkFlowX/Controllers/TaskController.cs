using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Models;

namespace WorkFlowX.Controllers
{
    public class TaskController : Controller
    {
        public Context _context = new Context();

        public TaskController()
        {
            _context.Database.CreateIfNotExists();
        }
        // GET: Task
        public ActionResult Index(int? pageSize, int? page)
        {
            var currentUser = (User)Session["User"];
            var userTeam = _context.Teams.Include("Users").FirstOrDefault(t => t.Users.Any(u => u.Id == currentUser.Id));
            var taskList = _context.Tasks.Include("Project").Include("Project.Teams").Where(t => t.Project.Teams.Any(tm => tm.Id == userTeam.Id)).ToList();
            page = pageSize < taskList.Count() ? page ?? 1 : 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            return View(taskList.ToPagedList(page.Value, pageSize.Value));
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            var task = _context.Tasks.Include("Project").Include("Project.Teams").FirstOrDefault(t => t.Id == id);
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create(int projectId)
        {
            ViewBag.pId = projectId;
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(int projectId, Task task)
        {
            try
            {
                var currentUser = (User)Session["User"];
                task.ProjectId = projectId;
                task.StartDate = DateTime.Now;
                task.UserId = currentUser.Id;
                _context.Tasks.Add(task);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = _context.Tasks.Include("Project").Include("Project.Teams").FirstOrDefault(t => t.Id == id);
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            try
            {
                var currentTask = _context.Tasks.Include("Project").Include("Project.Teams").FirstOrDefault(t => t.Id == id);
                currentTask.Name = task.Name;
                currentTask.Description = task.Description;
                currentTask.EndDate = task.EndDate;
                currentTask.ProjectId = task.ProjectId != 0 ? task.ProjectId : currentTask.ProjectId;
                currentTask.StartDate = task.StartDate;
                currentTask.Status = task.Status;
                currentTask.UserId = task.UserId != 0 ? task.UserId : currentTask.UserId;
                currentTask.ExpectedEndDate = task.ExpectedEndDate;
                currentTask.Priority = task.Priority;
                currentTask.Comment = task.Comment;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var deletedTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
                _context.Tasks.Remove(deletedTask);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
