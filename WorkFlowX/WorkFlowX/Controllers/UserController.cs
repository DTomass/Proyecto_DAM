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
    public class UserController : Controller
    {
        public Context _context = new Context();

        public UserController()
        {
            _context.Database.CreateIfNotExists();
        }
        [AuthorizeUser(license: 5)]
        // GET: User
        public ActionResult Index()
        {
            var usersList = _context.Users.Include("Company").Include("Role").ToList();
            return View(usersList);
        }

        [AuthorizeUser(license: 3)]
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [AuthorizeUser(license: 2)]
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        [AuthorizeUser(license:4)]
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
