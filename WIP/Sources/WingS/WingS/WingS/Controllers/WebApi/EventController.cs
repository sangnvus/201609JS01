using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class EventController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEventDetailById(int id)
        {
            EventBasicInfo EvtBasicInfo = new EventBasicInfo();
            try { 
                using (var db = new EventDAL()){
                EvtBasicInfo = db.GetEventBasicInfoById(id);
                }
            }catch(Exception)
            {
                return Redirect("/#/Error");
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = EvtBasicInfo });
        }
        //Get EventTimeLine       
        [HttpGet]
        public IHttpActionResult GetEventTimeLineByEventId(int id)
        {
            List <EventTimeLineBasicInfo> eventTimeLine = new List<EventTimeLineBasicInfo>();
            var EventList = new List<EventTimeLine>();
            using (var db = new EventDAL())
            {
                EventList = db.GetEventTimeLineByEventId(id);
                foreach(var eTimeLine in EventList)
                {
                    eventTimeLine.Add(new EventTimeLineBasicInfo
                    {
                        FromDate = eTimeLine.FromDate.ToString("dd/MM/yy"),
                        ToDate = eTimeLine.ToDate.ToString("dd/MM/yy"),
                        Content = eTimeLine.Content
                    });
                }
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = eventTimeLine });
        }
        //Get top view event
        [HttpGet]
        public IHttpActionResult GetTopFourEventByPoint()
        {
            List<Event> topEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            using (var db = new EventDAL())
            {
                //Get top event.
                topEvent = db.GetTopFourEventByPoint(4);
                foreach(Event e in topEvent)
                {
                    //Lấy ra ảnh tương ứng với mỗi 1 event với Status = 1
                    //Note: ảnh có status bằng 1 là ảnh dùng để hiển thị trên trang Home
                    var eventMainImage = db.GetMainImageEventById(e.EventID);

                    basicEventList.Add(new EventBasicInfo
                    {
                        CreatedDate = e.Created_Date.ToString("H:mm:ss dd/MM/yy"),
                        EventID = e.EventID,
                        EventName = e.EventName,
                        Content = e.Description,
                        CreatorID = e.CreatorID,
                        ImageUrl = eventMainImage.ImageUrl,
                        Status = e.Status
                    });
                }
            }
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= basicEventList });
        }
        //Get top 1 view event
        [HttpGet]
        [ActionName("Top1View")]
        public IHttpActionResult GetTop1ViewedEvent()
        {
            List<Event> topEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    //Get top event.
                    topEvent = db.GetTopFourEventByPoint(1);
                    foreach (Event e in topEvent)
                    {
                        //Lấy ra ảnh tương ứng với mỗi 1 event với Status = true
                        //Note: ảnh có status bằng "true" là ảnh dùng để hiển thị trên trang Event
                        var eventMainImage = db.GetMainImageEventById(e.EventID);

                        basicEventList.Add(new EventBasicInfo
                        {
                            CreatedDate = e.Created_Date.ToString("H:mm:ss dd/MM/yy"),
                            EventID = e.EventID,
                            EventName = e.EventName,
                            Content = e.Description,
                            CreatorID = e.CreatorID,
                            ImageUrl = eventMainImage.ImageUrl,
                            Status = e.Status
                        });
                    }
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {

                return Redirect("/#/Error");
            }

        }
        // Get list event by create date
        [HttpGet]
        [ActionName("NewestEvent")]
        public IHttpActionResult GetNewestEvent()
        {
            List<Event> NewestEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    NewestEvent = db.GetNewestEventByCreatedDate();
                    foreach (Event events in NewestEvent)
                    {
                        var eventMainImage = db.GetMainImageEventById(events.EventID);
                        basicEventList.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            EventName = events.EventName,
                            ImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            Status = true,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss dd/MM/yy")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }

    }
}
