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
        public ActionResult DashBoard()
        {
            return PartialView("~/Views/Admin/_DashBoard.cshtml");
        }

        public ActionResult AdminLogin()
        {
            return PartialView("~/Views/Admin/_AdminLogin.cshtml");
        }
        public ActionResult UserDashBoard()
        {
            return PartialView("~/Views/Admin/UserManage/_UserDashBoard.cshtml");
        }
        public ActionResult UserList()
        {
            return PartialView("~/Views/Admin/UserManage/_UserList.cshtml");
        }
        public ActionResult UserProfile()
        {
            return PartialView("~/Views/Admin/UserManage/_UserProfile.cshtml");
        }
        public ActionResult ThreadDashBoard()
        {
            return PartialView("~/Views/Admin/ThreadManage/_ThreadDashBoard.cshtml");
        }
        public ActionResult ThreadList()
        {
            return PartialView("~/Views/Admin/ThreadManage/_ThreadList.cshtml");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}