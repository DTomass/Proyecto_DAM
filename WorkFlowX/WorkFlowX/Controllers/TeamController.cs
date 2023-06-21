using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Models;

namespace WorkFlowX.Controllers
{
    public class TeamController : Controller
    {
        public Context _context = new Context();
        public TeamController() {
            _context.Database.CreateIfNotExists();
        }
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
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

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team/Edit/5
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

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team/Delete/5
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

        public ActionResult MyTeam()
        {
            var currenUser = (User)Session["User"];
            var myTeam = _context.Teams.FirstOrDefault(t => t.Id == currenUser.TeamId);
            ViewBag.MyTeam = myTeam;
            ViewBag.MyCompanions = _context.Users.Include("Company").Include("Role").Where(u => u.TeamId == currenUser.TeamId).ToList();
            ViewBag.TeamManager = _context.Users.FirstOrDefault(u => u.Id == myTeam.UserId).UserName;
            return View();
        }
    }
}
