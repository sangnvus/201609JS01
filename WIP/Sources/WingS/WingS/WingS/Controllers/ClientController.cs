using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Controllers
{
    [RequireHttps]
    public class ClientController : Controller
    {
        public ActionResult Home()
        {
            return PartialView("~/Views/Home/_Home.cshtml");
        }
    }
}