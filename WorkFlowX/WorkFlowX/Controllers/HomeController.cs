using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Filters;

namespace WorkFlowX.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser(license:1)]
        public ActionResult Index()
        {
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