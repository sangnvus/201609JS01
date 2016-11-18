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
    public class UserController : ApiController
    {
        public IHttpActionResult GetCurrentUserId()
        {
            UserBasicInfoDTO currentUser;
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return
                        Ok(new HTTPMessageDTO
                        {
                            Status = WsConstant.HttpMessageType.ERROR,
                            Message = "",
                            Type = WsConstant.HttpMessageType.NOT_AUTHEN
                        });
                }
                using (var db = new UserDAL())
                    currentUser = db.GetUserBasicInfo(User.Identity.Name);
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = WsConstant.HttpMessageType.BAD_REQUEST
                });
            }

            return Ok(new HTTPMessageDTO
            {
                Status = WsConstant.HttpMessageType.SUCCESS,
                Message = "",
                Type = "",
                Data = currentUser.UserId
            });
        }

        [HttpGet]
        public IHttpActionResult CheckLoginStatus()
        {
            UserBasicInfoDTO currentUser;

            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return
                        Ok(new HTTPMessageDTO
                        {
                            Status = WsConstant.HttpMessageType.ERROR,
                            Message = "",
                            Type = WsConstant.HttpMessageType.NOT_AUTHEN
                        });
                }
                using (var db = new UserDAL())
                {
                    currentUser = db.GetUserBasicInfo(User.Identity.Name);
                }
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = WsConstant.HttpMessageType.BAD_REQUEST
                });
            }

            return Ok(new HTTPMessageDTO
            {
                Status = WsConstant.HttpMessageType.SUCCESS,
                Message = "",
                Type = "",
                Data = currentUser
            });
        }
        public IHttpActionResult GetCurrentUser (string userName)
        {
            try
            {
                UserBasicInfoDTO userInfo = new UserBasicInfoDTO();
                using (var db = new UserDAL())
                {
                    userInfo = db.GetUserInfoUsingUserNameOrEmail(userName);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = userInfo
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }
       [HttpPut]
        public IHttpActionResult UpdateUserInfo(UserBasicInfoDTO UpdateUser)
        {
            //Call to accesslayer

            //Need to validate value
            return Ok(new HTTPMessageDTO
            {
                Status = WsConstant.HttpMessageType.SUCCESS,
                Message = "",
                Type = "",
                Data = UpdateUser
            });
        }
    }

}
