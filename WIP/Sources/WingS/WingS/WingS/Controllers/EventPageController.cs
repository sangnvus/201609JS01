using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class EventPageController : Controller
    {
        [HttpPost]
        public ActionResult CreateEvent(CreateEventInfo eventInfo,  IEnumerable<HttpPostedFileBase> images)
        {
            //Get Event Time Lines
            string[] FromDate = Request.Form.GetValues("FromDate");
            string[] ToDate = Request.Form.GetValues("ToDate");
            string[] Description = Request.Form.GetValues("Description");
            List<CreateEventSchedule> eventTimeLine = new List<CreateEventSchedule>();
            if (FromDate!= null && FromDate.Length>=1)
            {
                for(int i = 0; i<FromDate.Length;i++)
                {
                    if (FromDate[i] != "" && ToDate[i] != "" && Description[i] !="")
                    {
                        eventTimeLine.Add(new CreateEventSchedule(FromDate[i],ToDate[i],Description[i]));
                    }
                }
            }
            Event newEvent = new Event();
            using (var db = new EventDAL())
            {
                newEvent = db.AddNewEvent(eventInfo);
                foreach (var scheduleInfo in eventTimeLine)
                {
                    db.AddNewEventTimeLine(scheduleInfo, newEvent.EventID);
                }
            }
            //Add Image to server
            try
            {
                foreach (HttpPostedFileBase img in images)
                {
                    //rebuild image name
                    string imageName = WsConstant.randomString() + Path.GetExtension(img.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), imageName);
                    img.SaveAs(path);
                    string imgaeUrl = "Content/Upload/" + imageName;
                    //Add Image to db.
                    using (var db = new AlbumImageDAL())
                    {
                        db.AddEventAlbum(new EventAlbumImageDTO(newEvent.EventID, imgaeUrl));
                    }
                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }
            return Redirect("/#/Home");
        }

    }
}