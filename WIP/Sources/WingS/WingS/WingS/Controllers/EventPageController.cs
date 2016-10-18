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
        public ActionResult CreateEvent(CreateEventInfo eventInfo, IEnumerable<HttpPostedFileBase> images)
        {
            Event newEvent = null;
            using (var db = new EventDAL())
            {
                newEvent = db.AddNewEvent(eventInfo);
                foreach (var scheduleInfo in AddScheduleToList(eventInfo))
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

        public List<CreateEventSchedule> AddScheduleToList(CreateEventInfo eventInfo)
        {
            var timelineList = new List<CreateEventSchedule>();
            if (eventInfo.FromDate1 != "" && eventInfo.ToDate1 != "" && eventInfo.Description1 != "")
            {
                timelineList.Add(new CreateEventSchedule(eventInfo.FromDate1, eventInfo.ToDate1, eventInfo.Description1));
                if (eventInfo.FromDate2 != "" && eventInfo.ToDate2 != "" && eventInfo.Description2 != "")
                {
                    timelineList.Add(new CreateEventSchedule(eventInfo.FromDate2, eventInfo.ToDate2, eventInfo.Description2));
                    if (eventInfo.FromDate3 != "" && eventInfo.ToDate3 != "" && eventInfo.Description3 != "")
                    {
                        timelineList.Add(new CreateEventSchedule(eventInfo.FromDate3, eventInfo.ToDate3, eventInfo.Description3));
                        if (eventInfo.FromDate4 != "" && eventInfo.ToDate4 != "" && eventInfo.Description4 != "")
                        {
                            timelineList.Add(new CreateEventSchedule(eventInfo.FromDate4, eventInfo.ToDate4, eventInfo.Description4));
                            if (eventInfo.FromDate5 != "" && eventInfo.ToDate5 != "" && eventInfo.Description5 != "")
                            {
                                timelineList.Add(new CreateEventSchedule(eventInfo.FromDate5, eventInfo.ToDate5, eventInfo.Description5));
                                if (eventInfo.FromDate6 != "" && eventInfo.ToDate6 != "" && eventInfo.Description6 != "")
                                {
                                    timelineList.Add(new CreateEventSchedule(eventInfo.FromDate6, eventInfo.ToDate6, eventInfo.Description6));
                                    if (eventInfo.FromDate7 != "" && eventInfo.ToDate7 != "" && eventInfo.Description7 != "")
                                    {
                                        timelineList.Add(new CreateEventSchedule(eventInfo.FromDate7, eventInfo.ToDate7, eventInfo.Description7));
                                        if (eventInfo.FromDate8 != "" && eventInfo.ToDate8 != "" && eventInfo.Description8 != "")
                                        {
                                            timelineList.Add(new CreateEventSchedule(eventInfo.FromDate8, eventInfo.ToDate8, eventInfo.Description8));
                                            if (eventInfo.FromDate9 != "" && eventInfo.ToDate9 != "" && eventInfo.Description9 != "")
                                            {
                                                timelineList.Add(new CreateEventSchedule(eventInfo.FromDate9, eventInfo.ToDate9, eventInfo.Description9));
                                                if (eventInfo.FromDate10 != "" && eventInfo.ToDate10 != "" && eventInfo.Description10 != "")
                                                {
                                                    timelineList.Add(new CreateEventSchedule(eventInfo.FromDate10, eventInfo.ToDate10, eventInfo.Description10));
                                                }
                                                else
                                                {
                                                    return timelineList;
                                                }
                                            }
                                            else
                                            {
                                                return timelineList;
                                            }
                                        }
                                        else
                                        {
                                            return timelineList;
                                        }
                                    }
                                    else
                                    {
                                        return timelineList;
                                    }
                                }
                                else
                                {
                                    return timelineList;
                                }
                            }
                            else
                            {
                                return timelineList;
                            }
                        }
                        else
                        {
                            return timelineList;
                        }
                    }
                    else
                    {
                        return timelineList;
                    }
                    
                }
                else
                {
                    return timelineList;
                }
                
            }
            return timelineList;
        }
    }
}