using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class NotificationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllNotification()
        {
            List<NotificationBasicInfoDTO> NotificationList;
            using (var db = new NotificationDAL())
            {
                NotificationList = db.GetAllNotificationByUserName(User.Identity.Name);
            }
            if (NotificationList != null)
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = NotificationList});
            else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
        }
    }
}
