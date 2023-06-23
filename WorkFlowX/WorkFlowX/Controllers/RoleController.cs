using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Filters;
using WorkFlowX.Models;
using WorkFlowX.Models.Dtos;

namespace WorkFlowX.Controllers
{
    public class RoleController : Controller
    {
        public Context _context = new Context();

        public RoleController()
        {
            _context.Database.CreateIfNotExists();
        }
        // GET: Role
        [AuthorizeUser(license: 10)]
        public ActionResult Index(int? pageSize, int? page)
        {
            var rolesList = _context.Roles.Include("Licenses").ToList();
            page = pageSize < rolesList.Count() ? page ?? 1 : 1;
            pageSize = pageSize ?? 5;
            ViewBag.PageSize = pageSize;
            return View(rolesList.ToPagedList(page.Value, pageSize.Value));
        }

        // GET: Role/Details/5
        [AuthorizeUser(license: 8)]
        public ActionResult Details(int id)
        {
            var role = _context.Roles.Include("Licenses.Area").FirstOrDefault(r => r.Id == id);
            var roleDto = Mapper.Map<RoleDTO>(role);
            return View(roleDto);
        }

        // GET: Role/Create
        [AuthorizeUser(license: 6)]
        public ActionResult Create()
        {
            var licenses = _context.Licenses.Include("Area").ToList();
            var licensesDto = Mapper.Map<List<LicenseDTO>>(licenses);
            ViewBag.Licenses = licensesDto;
            ViewBag.SelectedLicenses = _context.Licenses.ToList();
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleDTO roleDTO)
        {
            try
            {
                _context.Roles.Add(Mapper.Map<Role>(roleDTO));
                _context.SaveChanges();

                if(roleDTO.LicenseIds != null)
                {
                    var role = _context.Roles.Include("Licenses").FirstOrDefault(r => r.RoleName == roleDTO.RoleName);
                    foreach (var licenseId in roleDTO.LicenseIds)
                    {
                        var license = _context.Licenses.FirstOrDefault(l => l.Id == licenseId);
                        role.Licenses.Add(license);
                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        [AuthorizeUser(license: 7)]
        public ActionResult Edit(int id)
        {
            var roleDTO = Mapper.Map<RoleDTO>(_context.Roles.Include("Licenses.Area").FirstOrDefault(r => r.Id == id));
            ViewBag.Licenses = Mapper.Map<List<LicenseDTO>>(_context.Licenses.Include("Area").ToList());
            ViewBag.SelectedLicenses = roleDTO.Licenses.ToList();
            return View(roleDTO);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RoleDTO roleDTO)
        {
            try
            {
                var currentRole = _context.Roles.Include("Licenses.Area").FirstOrDefault(r => r.Id == id);
                currentRole.RoleName = roleDTO.RoleName;
                currentRole.Licenses.Clear();
                if (roleDTO.LicenseIds != null)
                {
                    foreach (var licenseId in roleDTO.LicenseIds)
                    {
                        var license = _context.Licenses.FirstOrDefault(l => l.Id == licenseId);
                        currentRole.Licenses.Add(license);
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

        // GET: Role/Delete/5
        [AuthorizeUser(license: 9)]
        public ActionResult Delete(int id)
        {
            var roleDto = Mapper.Map<RoleDTO>(_context.Roles.Include("Licenses.Area").FirstOrDefault(r => r.Id == id));
            return View(roleDto);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var deletedRole = _context.Roles.Include("Licenses").FirstOrDefault(r => r.Id == id);
                _context.Roles.Remove(deletedRole);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AuthorizeUser(license: 10)]
        public ActionResult GetLicensesFromRoleId(int? roleId)
        {
            var licenses = roleId != null ? _context.Roles.Include("Licenses.Area").FirstOrDefault(r => r.Id == roleId).Licenses : null;
            List<LicenseDTO> licensesDto = licenses != null ? Mapper.Map<List<LicenseDTO>>(licenses) : null;

            return Json(licensesDto, JsonRequestBehavior.AllowGet);
        }
    }
}
