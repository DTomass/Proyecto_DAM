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
            return View();
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
