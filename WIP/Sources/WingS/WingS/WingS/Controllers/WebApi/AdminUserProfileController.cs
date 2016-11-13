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
    public class AdminUserProfileController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetUserProfileWithId(int userId)
        {
            try
            {
                UserBasicInfoDTO currentUser;

                using (var db = new UserDAL())
                {
                    currentUser = db.GetFullInforOfUserAsBasicUser(userId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get User Profile Successfully",
                    Type = "",
                    Data = currentUser
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get User Profile!",
                    Type= ""
                });
            }
        }
    }
}
