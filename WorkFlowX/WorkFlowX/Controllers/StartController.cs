using System.Web.Mvc;
using WorkFlowX.Data;
using WorkFlowX.Models;
using WorkFlowX.Services;

namespace WorkFlowX.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string mail, string password)
        {
            DtoUser user = DBUser.Validate(mail, UtilityService.ConvertToSHA256(password));
            if (user != null)
            {
                if(!user.Confirmation)
                {
                    ViewBag.Mensaje = $"Please confirm your mail at {mail}";
                }else if(user.Restore)
                {
                    ViewBag.Mensaje = $"Please restore your password at {mail}";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Mensaje = "Invalid mail or password";
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(DtoUser user)
        {
            if (user.UserPassword != user.ConfirmPassword)
            {
                ViewBag.Nombre = user.UserName;
                ViewBag.Correo = user.UserMail;
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            if (DBUser.Find(user.UserMail) == null)
            {
                user.UserPassword = UtilityService.ConvertToSHA256(user.UserPassword);
                user.Token = UtilityService.GenerateToken();
                user.Restore = false;
                user.Confirmation = false;
                bool response = DBUser.Register(user);

                if (response)
                {
                    string path = HttpContext.Server.MapPath("~/Template/Confirm.html");
                    string content = System.IO.File.ReadAllText(path);
                    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Start/Confirm?token=" + user.Token);

                    string htmlBody = string.Format(content, user.UserName, url);

                    DtoMail mail = new DtoMail()
                    {
                        MailTo = user.UserMail,
                        MailSubject = "Correo confirmacion",
                        MailBody = htmlBody
                    };

                    bool enviado = MailService.SendMail(mail);
                    ViewBag.Creado = true;
                    ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {user.UserMail} para confirmar su cuenta";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo crear su cuenta";
                }



            }
            else
            {
                ViewBag.Mensaje = "El correo ya se encuentra registrado";
            }


            return View();
        }

        public ActionResult Confirm(string token)
        {
            ViewBag.Respuesta = DBUser.Confirm(token);
            return View();
        }

        public ActionResult Restore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Restore(string mail)
        {
            DtoUser user = DBUser.Find(mail);
            ViewBag.Correo = mail;
            if (user != null)
            {
                bool response = DBUser.Restore(1, user.UserPassword, user.Token);

                if (response)
                {
                    string path = HttpContext.Server.MapPath("~/Template/Restore.html");
                    string content = System.IO.File.ReadAllText(path);
                    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Start/Update?token=" + user.Token);

                    string htmlBody = string.Format(content, user.UserName, url);

                    DtoMail dtoMail = new DtoMail()
                    {
                        MailTo = mail,
                        MailSubject = "Restablecer cuenta",
                        MailBody = htmlBody
                    };

                    bool send = MailService.SendMail(dtoMail);
                    ViewBag.Restablecido = send;
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo restablecer la cuenta";
                }

            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias con el correo";
            }

            return View();
        }

        public ActionResult Update(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public ActionResult Update(string token, string password, string confirmPassword)
        {
            ViewBag.Token = token;
            if (password != confirmPassword)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            bool response = DBUser.Restore(0, UtilityService.ConvertToSHA256(password), token);

            if (response)
                ViewBag.Restablecido = true;
            else
                ViewBag.Mensaje = "No se pudo actualizar";

            return View();
        }
    }
}