using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult UserDashBoard()
        {
            return PartialView("~/Views/Admin/_UserDashBoard.cshtml");
        }

        public ActionResult AdminLogin()
        {
            return PartialView("~/Views/Admin/_AdminLogin.cshtml");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}