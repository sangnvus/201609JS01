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
                using(var db = new UserDAL())
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
                Data = currentUser
            });
        }
        public IHttpActionResult GetCurrentUser (string userName)
        {
            UserBasicInfoDTO userInfo = new UserBasicInfoDTO();
            using(var db = new UserDAL() )
            {
                var user = db.GetUserInfo(userName);
                {
                    userInfo.UserName = user.UserName;
                    userInfo.Email = user.Email;
                    userInfo.FullName = user.FullName;
                    userInfo.Gender = user.Gender;
                    userInfo.Phone = user.Phone;
                    userInfo.Country = user.Country;
                    userInfo.Address = user.Address;
                    userInfo.ProfileImage = user.ProfileImage;
                    userInfo.CreateDate = user.CreateDate;
                    userInfo.DOB = user.DOB;
                    userInfo.Point = user.Point;
                }
            }
            using(var db = new ThreadDAL() )
            {
                userInfo.NumberOfPost = db.GetNumberOfPostPerUser(userName);
            }

            
            return Ok(new HTTPMessageDTO
            {
                Status = WsConstant.HttpMessageType.SUCCESS,
                Message = "",
                Type = "",
                Data = userInfo
            });
        }
    }

}
