using System;
using System.Web;
using System.Web.Http;
using API_NganLuong;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;
using System.Collections.Generic;

namespace WingS.Controllers
{
    public class DonationLoadController : ApiController
    {
        /// <summary>
        /// Get Donate Info
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDonateInfo(int eventId)
        {
            try
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
                    if (eventNmGet == "")
                    {
                        return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.NOT_FOUND});
                    }
                    else
                    {
                        userInfo.DonatedEventName = eventNmGet;
                    }

                }
                return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.SUCCESS, Data = userInfo});
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR});
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllDonationBasicInformation()
        {
            try
            {
                List<DonationDTO> listDonation;
                using (var db = new DonationDAL())
                {
                    listDonation = db.GetAllDonation();
                }
                return Ok(new HTTPMessageDTO 
                { 
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Data = listDonation 
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Donation Information",
                    Type = ""
                });
            }
        }
    }
}
