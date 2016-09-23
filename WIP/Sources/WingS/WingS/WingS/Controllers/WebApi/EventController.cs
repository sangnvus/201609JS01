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
            EventBasicInfo GetEvent = new EventBasicInfo();
            Event topEvent = null ;
            using (var db = new EventDAL())
            {
                //Get top event.
                topEvent = db.GetTopViewEvent();
                //Set basic info to event
                if (topEvent != null)
                {
                    GetEvent.EventID = topEvent.EventID;
                    GetEvent.EventName = topEvent.EventName;
                    GetEvent.Content = topEvent.Description;
                    GetEvent.CreatorID = topEvent.CreatorID;
                    GetEvent.ImageUrl = topEvent.ImageUrl;
                }
            }
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= GetEvent });
        }
    }
}
