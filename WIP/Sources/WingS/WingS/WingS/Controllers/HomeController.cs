using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.Models;
using WingS.Repository;

namespace WingS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Ws_Datacontext())
            {
                WS_User ws = db.WS_Users.Find(1);
            }
             
                return View();
        }

      
    }
}