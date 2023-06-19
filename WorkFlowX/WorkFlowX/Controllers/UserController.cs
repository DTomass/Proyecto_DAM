using System.Linq;
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
            var user = _context.Users.Include("Company").Include("Role").FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [AuthorizeUser(license: 2)]
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _context.Users.Include("Company").Include("Role").FirstOrDefault(u => u.Id == id);

            // Obtener la lista de compañías disponibles, puedes ajustar esto según tu lógica
            var companyList = _context.Companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CompanyName
            });

            var roleList = _context.Roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.RoleName
            });

            ViewBag.CompanyList = companyList;
            ViewBag.RoleList = roleList;

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User modifiedUser)
        {
            try
            {
                //Add changes
                var actualUser = _context.Users.FirstOrDefault(u => u.Id == id);
                actualUser.UserName = modifiedUser.UserName;
                actualUser.UserMail = modifiedUser.UserMail;
                actualUser.UserPassword = modifiedUser.UserPassword;
                actualUser.NeedRestore = modifiedUser.NeedRestore;
                actualUser.HasConfirmation = modifiedUser.HasConfirmation;
                actualUser.Token = modifiedUser.Token;
                actualUser.CompanyId = modifiedUser.CompanyId;
                actualUser.RoleId = modifiedUser.RoleId;
                actualUser.UserSurname = modifiedUser.UserSurname;
                actualUser.UserPhone = modifiedUser.UserPhone;
                actualUser.Birthdate = modifiedUser.Birthdate;
                actualUser.Gender = modifiedUser.Gender;
                actualUser.UserAddress = modifiedUser.UserAddress;
                actualUser.Role = _context.Roles.FirstOrDefault(u => u.Id == modifiedUser.RoleId);
                actualUser.Company = _context.Companies.FirstOrDefault(u => u.Id != modifiedUser.CompanyId);

                _context.SaveChanges();


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
            var user = _context.Users.Include("Company").Include("Role").FirstOrDefault(u => u.Id == id);
            return View(user);
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
