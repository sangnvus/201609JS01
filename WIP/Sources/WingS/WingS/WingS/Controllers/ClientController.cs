using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Controllers
{
    public class ClientController : Controller
    {
        public ActionResult Home()
        {
            return PartialView("~/Views/Home/_Home.cshtml");
        }
        public ActionResult Login()
        {
            return PartialView("~/Views/Login/_Login.cshtml");
        }
        public ActionResult Register()
        {
            return PartialView("~/Views/Login/_Register.cshtml");
        }
        public ActionResult ForgotPassword()
        {
            return PartialView("~/Views/Login/_ForgotPassword.cshtml");
        }
    }
}