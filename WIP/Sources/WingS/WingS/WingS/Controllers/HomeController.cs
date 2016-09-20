using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.Models;
using WingS.DataHelper;
using System.Web.Security;

namespace WingS.Controllers
{
    public class HomeController : ModelAccess
    {
        public ActionResult Index()
        {
           GetCurrentUser();
           ViewBag.Title = WsConstant.PageTitle.Home;
           return View();
        }

      
    }
}