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

namespace WingS.Controllers.WebApi
{
    public class EventController : ApiController
    {
        //Get top view event
        [HttpGet]
        public IHttpActionResult GetTopViewEvent()
        {
            UserBasicInfoDTO GetUser = null;
            Ws_User user = null;
            using (var db = new Ws_DataContext())
            {
                user = (Ws_User)db.Ws_User.Where(x => x.UserID == 1);
                //Get top event.
                //Set basic info to event
                GetUser.UserName = user.UserName;
                GetUser.IsActive = user.IsActive;
                GetUser.Email = user.Email;
              
            }
            //EventBasicInfo GetEvent = null;
            //Event topEvent = null;
            //using (var db = new Ws_DataContext())
            //{
            //    topEvent = (Event)db.Events.Where(x => x.Status == true).OrderByDescending(x => x.Event_Statistic.Views).Take(1);
            //    //Get top event.
            //    //Set basic info to event
            //    if(topEvent!= null)
            //    {
            //        GetEvent.EventID = topEvent.EventID;
            //        GetEvent.EventName = topEvent.EventName;
            //        GetEvent.CreatorID = topEvent.CreatorID;
            //        GetEvent.Content = topEvent.Description;
            //    }
            //}
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= GetUser });
        }
    }
}
