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
    }
}