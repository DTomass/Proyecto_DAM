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
    public class CompanyController : Controller
    {
        public Context _context = new Context();

        public CompanyController()
        {
            _context.Database.CreateIfNotExists();
        }
        // GET: Company
        public ActionResult Index(int? pageSize, int? page)
        {
            var companyList = _context.Companies.ToList();
            page = pageSize < companyList.Count() ? page ?? 1 : 1;
            pageSize = pageSize ?? 5;
            ViewBag.PageSize = pageSize;
            return View(companyList.ToPagedList(page.Value, pageSize.Value));
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            var company = _context.Companies.FirstOrDefault(r => r.Id == id);
            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            var company = _context.Companies.FirstOrDefault(r => r.Id == id);
            return View(company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Company company)
        {
            try
            {
                var currentCompany = _context.Companies.FirstOrDefault(r => r.Id == id);
                currentCompany.CompanyName = company.CompanyName;
                currentCompany.CompanyAddress = company.CompanyAddress;
                currentCompany.CompanyPhone = company.CompanyPhone;
                currentCompany.CompanyMail = company.CompanyMail;
                currentCompany.CompanyWeb = company.CompanyWeb;
                currentCompany.CompanyNif = company.CompanyNif;
                currentCompany.RegisterDate = company.RegisterDate;
                currentCompany.CompanyCity = company.CompanyCity;
                currentCompany.CompanyCountry = company.CompanyCountry;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == id);
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var deletedCompany = _context.Companies.FirstOrDefault(c => c.Id == id);
                _context.Companies.Remove(deletedCompany);
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
