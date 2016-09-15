using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.AccessData;
using WingS.Models;

namespace WingS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Ws_DataContext())
            {
                Ws_User s = db.WingsUsers.Find(1);
            }
            return View();
        }

     
    }
}