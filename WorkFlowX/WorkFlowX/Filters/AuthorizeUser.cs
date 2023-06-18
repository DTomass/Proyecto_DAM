using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Models;

namespace WorkFlowX.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private User user;
        private Context db = new Context();
        private int licenseId;

        public AuthorizeUser(int license)
        {
            licenseId = license;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var neededLicense = db.Licenses.Include(l => l.Area).FirstOrDefault(l => l.Id == licenseId);
            try
            {
                user = (User)HttpContext.Current.Session["User"];
                user = db.Users.Include(u => u.Role.Licenses).FirstOrDefault(u => u.Id == user.Id);

                if (!user.Role.Licenses.Contains(neededLicense))
                {
                    filterContext.Result = new RedirectResult("~/Home/NoAuthorized?licenseName="+neededLicense.Name+"&areaName="+neededLicense.Area.Name);
                }
            } catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Home/NoAuthorized?licenseName=" + neededLicense.Name + "&areaName=" + neededLicense.Area.Name);
            }
        }
    }
}