using System;
using System.Web;
using System.Web.Http;
using API_NganLuong;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class DonationLoadController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDonateInfo(int eventId)
        {
            var userInfo = new UserBasicInfoDTO();
            using (var db = new UserDAL())
            {
                var user = db.GetUserByUserNameOrEmail(User.Identity.Name);
                var userGet = db.GetUserInformation(user.UserID);
                userInfo.FullName = userGet.FullName;
                userInfo.Email = user.Email;
                userInfo.Phone = userGet.Phone;
                userInfo.UserId = user.UserID;
                userInfo.DonatedEventId = eventId;
            }
            using (var db2 = new EventDAL())
            {
                var eventNmGet = db2.GetEventNameById(eventId);
                if (eventNmGet=="")
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                }
                else
                {
                    userInfo.DonatedEventName = eventNmGet;
                }

            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = userInfo });
        }
    }
}
