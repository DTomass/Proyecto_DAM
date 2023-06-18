using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowX.Controllers;
using WorkFlowX.Models;

namespace WorkFlowX.Filters
{
    public class VerificationSession : ActionFilterAttribute
    {
        private User user { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try 
            { 
                base.OnActionExecuting(filterContext);

                user = (User)HttpContext.Current.Session["User"];
                if (user == null)
                {
                    if(filterContext.Controller is StartController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Start/Login");
                    }
                }

                base.OnActionExecuting(filterContext);
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Start/Login");
            }
        }
    }
}