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
        //Get top view event
        [HttpGet]
        public IHttpActionResult GetTopViewEvent()
        {
            List<Event> topEvent = null;
            var basicList = new List<EventBasicInfo>();
            using (var db = new EventDAL())
            {
                //Get top event.
                topEvent = db.GetTop3EventByView();
                foreach(Event e in topEvent)
                {
                    basicList.Add(new EventBasicInfo { EventID = e.EventID, EventName = e.EventName, Content = e.Description, CreatorID = e.CreatorID, ImageUrl = e.ImageUrl, Status = e.Status });
                }
            }
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= basicList });
        }
    }
}
