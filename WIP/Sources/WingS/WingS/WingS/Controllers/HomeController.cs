using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.Models;

namespace WingS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Ws_DataContext())
            {
                Ws_User s = db.Ws_User.Find(1);
            }
            return View();
        }

      
    }
}