using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WingS.DataAccess;

namespace WingS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void DetechExpiredEvent()
        {
            using (var db = new Ws_DataContext())
            {
                //Get All events
                var events = db.Events.Where(x => x.IsOpen == true && x.Status == true).ToList();
                foreach (var item in events)
                {
                   if(DateTime.Compare(DateTime.Now,item.Finish_Date)>0)
                    {
                        item.IsOpen = false;
                        db.Events.AddOrUpdate(item);
                    }
                }
                db.SaveChanges();
            }
        }
        protected void CalculateTotalPointForEvent()
        {
            using (var db = new Ws_DataContext())
            {
                //Get All events
                var events = db.Events.Where(x => x.IsOpen == true && x.Status == true).ToList();
                foreach (var item in events)
                {
                    //Get Current Pont;
                    var Points = item.TotalPoint;
                    //Get All donations in a event.
                    var donationList = db.Donations.Where(x => x.EventId == item.EventID);
                    foreach(var donation in donationList)
                    {
                       
                    }
                   //Get All Likes in event.
                   
                }
            }
          
        }
            
    }
}
