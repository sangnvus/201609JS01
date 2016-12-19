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

            // Dynamically create new timer
            System.Timers.Timer timScheduledTask = new System.Timers.Timer();

            // Timer interval is set in miliseconds,
            // Run a task every minute
            timScheduledTask.Interval = 60 * 1000;
            timScheduledTask.Enabled = true;

            // Add handler for Elapsed event
            timScheduledTask.Elapsed +=
            new System.Timers.ElapsedEventHandler(timScheduledTask_Elapsed);
        }
        protected void timScheduledTask_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            // Execute task: Check expire project
            if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 50)
            {
                // Check expire project
                DetechExpiredEvent();
           
            }

            // Execute task: Caculate popular point
            if (DateTime.Now.Hour == 23 && (DateTime.Now.Minute == 55))
            {
                // Caculate popular point
                CalculateTotalPointForEvent();
                CalculateTotalPointForOrg();
                CalculatePointsForUser();
            }


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
                    var totalPoints = item.TotalPoint;
                    //Get Current Pont;
                    var currentPoints = 0;
                    //Get All donations in a event.
                    var donationList = db.Donations.Where(x => x.EventId == item.EventID);
                    foreach(var donation in donationList)
                    {
                        currentPoints = currentPoints + (int)donation.DonatedMoney / 10000;
                    }
                    var numberOfLike = db.LikeEvents.Where(x => x.EventId == item.EventID);
                    currentPoints = currentPoints + numberOfLike.Count();

                    //  Minus 10 Point everyday if no more 5 points in a day
                    if ((currentPoints - totalPoints) < 5 && totalPoints > 50)
                    {
                        currentPoints = currentPoints - 5;
                    }

                    //Save changes total points for each event
                    item.TotalPoint = currentPoints;
                    db.Events.AddOrUpdate(item);
                }
                db.SaveChanges();
            }
          
        }
        protected void CalculateTotalPointForOrg()
        {
            using (var db = new Ws_DataContext())
            {
                //Get ALl Organizations.
                var listOrg = db.Organizations.Where(x => x.IsActive == true);
                foreach(var item in listOrg)
                {
                    int currentPoints = item.Point;
                    //Get All Event of each Orgs.
                    var donationList = db.Donations.Where(x => x.Event.Organization.OrganizationId == item.OrganizationId).Select(x => x.DonatedMoney);
                    if (donationList != null)
                    { 
                    foreach (var donation in donationList)
                    {
                        currentPoints = (int)donation/10000;
                    }
                    //Update new Point.
                    item.Point = currentPoints;
                    db.Organizations.AddOrUpdate(item);
                    }
                }
                db.SaveChanges();
            }
        }
        protected void CalculatePointsForUser()
        {
            using (var db = new Ws_DataContext())
            {
                var listUser = db.User_Information.Where(x => x.Ws_User.IsActive == true);

                foreach(var item in listUser)
                {
                    var totalPoint = item.Point;
                    var createdThread = db.Threads.Where(x => x.UserId == item.UserID).Count();
                    totalPoint = createdThread * 10;

                    var donation = db.Donations.Where(x => x.UserId == item.UserID).Select(x => x.DonatedMoney);
                    if(donation!=null)
                    {
                    foreach(var total in donation)
                    {
                        totalPoint += ((int)total / 10000);
                    }
                    item.Point = totalPoint;
                    db.User_Information.AddOrUpdate(item);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
