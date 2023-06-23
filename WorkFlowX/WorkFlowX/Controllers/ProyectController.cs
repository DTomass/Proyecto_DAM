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
    public class ProyectController : Controller
    {
        public Context _context = new Context();

        public ProyectController()
        {
            _context.Database.CreateIfNotExists();
        }
        // GET: Proyect
        public ActionResult Index(int? pageSize, int? page)
        {
            var currentUser = (User)Session["User"];
            var userTeam = _context.Teams.FirstOrDefault(t => t.Users.Any(u => u.Id == currentUser.Id));
            var proyectsList = _context.Projects.Include("Tasks").Include("Teams").Include("Company").Where(p => p.Teams.Any(t => t.Id == userTeam.Id)).ToList();
            page = pageSize < proyectsList.Count() ? page ?? 1 : 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            return View(proyectsList.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult ProjectTasksList(int projectId, int? pageSize, int? page)
        {
            var currentUser = (User)Session["User"];
            var userTeam = _context.Teams.FirstOrDefault(t => t.Users.Any(u => u.Id == currentUser.Id));
            var proyectsList = _context.Projects.Include("Tasks").FirstOrDefault(p => p.Id == projectId).Tasks.ToList();
            page = pageSize < proyectsList.Count() ? page ?? 1 : 1;
            pageSize = pageSize ?? 5;
            ViewBag.PageSize = pageSize;
            return View(proyectsList.ToPagedList(page.Value, pageSize.Value));
        }

        // GET: Proyect/Details/5
        public ActionResult Details(int id)
        {
            var proyect = _context.Projects.Include("Tasks").Include("Teams").Include("Company").FirstOrDefault(p => p.Id == id);
            return View(proyect);
        }

        // GET: Proyect/Create
        public ActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        // POST: Proyect/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                
                project.StartDate = DateTime.Now;
                _context.Projects.Add(project);
                _context.SaveChanges();
                if (project.TeamIds.Count > 0)
                {
                    foreach (var teamId in project.TeamIds)
                    {
                        var projectTeams = _context.Projects.Include("Teams").FirstOrDefault(p => p.Id == project.Id);
                        var team = _context.Teams.Include("Users").FirstOrDefault(t => t.Id == teamId);
                        projectTeams.Teams.Add(team);
                    }
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proyect/Edit/5
        public ActionResult Edit(int id)
        {
            var proyect = _context.Projects.Include("Tasks").Include("Teams").Include("Company").FirstOrDefault(p => p.Id == id);
            return View(proyect);
        }

        // POST: Proyect/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Project project)
        {
            try
            {
                var currentProyect = _context.Projects.FirstOrDefault(p => p.Id == id);
                currentProyect.Name = project.Name;
                currentProyect.Description = project.Description;
                currentProyect.StartDate = project.StartDate;
                currentProyect.EndDate = project.EndDate;
                currentProyect.ExpectedEndDate = project.ExpectedEndDate;
                currentProyect.ActualCost = project.ActualCost;
                currentProyect.Budget = project.Budget;
                currentProyect.CompanyId = project.CompanyId;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proyect/Delete/5
        public ActionResult Delete(int id)
        {
            var deletedProject = _context.Projects.Include("Tasks").Include("Teams").Include("Company").FirstOrDefault(p => p.Id == id);
            return View(deletedProject);
        }

        // POST: Proyect/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _context.Projects.Remove(_context.Projects.Include("Tasks").FirstOrDefault(p => p.Id == id));
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
