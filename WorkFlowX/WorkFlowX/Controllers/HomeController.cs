using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Filters;
using WorkFlowX.Models;

namespace WorkFlowX.Controllers
{
    public class HomeController : Controller
    {
        public Context _context = new Context();

        public HomeController()
        {
            _context.Database.CreateIfNotExists();
        }
        public ActionResult Index()
        {
            var currentUser = (User)Session["User"];
            var userTeam = _context.Teams.Include("Users").FirstOrDefault(t => t.Users.Any(u => u.Id == currentUser.Id));
            ViewBag.Tasks = _context.Tasks.Include("Project").Include("User").Include("Project.Teams").Where(t => t.Project.Teams.Any(tm => tm.Id == userTeam.Id)).OrderBy(t => t.EndDate).Take(3).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoAuthorized(string licenseName, string areaName) 
        {
            ViewBag.Message = "You are not authorized to access this page. You need to have the permission of " + licenseName + " in " + areaName;
            return View();
        }
    }
}