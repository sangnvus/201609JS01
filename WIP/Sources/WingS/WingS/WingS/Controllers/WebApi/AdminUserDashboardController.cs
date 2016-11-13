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
    public class AdminUserDashboardController : ApiController
    {
        /// <summary>
        /// Get user manage basisc information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserManageBasicInfor()
        {
            try
            {
                UserManageBasicInforDTO userManageInfor = new UserManageBasicInforDTO();
                
                using (var db = new UserDAL())
                {
                    userManageInfor.NumberActiveUser = db.CountUserActiveOrNot(true);
                    userManageInfor.NumberNotActiveUser = db.CountUserActiveOrNot(false);
                    userManageInfor.NumberVerifyUser = db.CountUserVerifyOrNot(true);
                    userManageInfor.NumberNotVerifyUser = db.CountUserVerifyOrNot(false);
                    userManageInfor.NumberNewCreateUser = db.CountNewUser();
                    userManageInfor.NumberTotalUser = db.CountTotalUser();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = userManageInfor
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

        [HttpGet]
        public IHttpActionResult GetNewCreatedUser()
        {
            try
            {
                var userListBasic = new List<UserBasicInfoDTO>();

                using (var db = new UserDAL())
                {
                    List<Ws_User> userList = db.GetNewUser();
                    foreach (Ws_User user in userList)
                    {
                        //Get information of user
                        UserBasicInfoDTO userBasic = db.GetUserInfo(user.UserName);
                        
                        //get number of post
                        int numberOfPost;
                        using (var dbThread = new ThreadDAL())
                        {
                            numberOfPost=dbThread.GetNumberOfPostPerUser(user.UserID);
                        }

                        userListBasic.Add(new UserBasicInfoDTO
                        {
                            UserName = user.UserName,
                            AccountType = user.AccountType,
                            IsActive = user.IsActive,
                            IsVerify = user.IsVerify,
                            FullName = userBasic.FullName,
                            ProfileImage = userBasic.ProfileImage,
                            Email = user.Email,
                            Gender = userBasic.Gender,
                            Phone = userBasic.Phone,
                            Address = userBasic.Address,
                            NumberOfPost = numberOfPost,
                            DOB = userBasic.DOB,
                            Country = userBasic.Country,
                            CreateDate = user.CreatedDate.ToString("H:mm:ss dd/MM/yy"),
                            Point = userBasic.Point,
                        });
                    }

                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = userListBasic
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

        /// <summary>
        /// Get top Donator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopMostDonatedUser()
        {
            try
            {
                var topTenDonate = new List<UserBasicInfoDTO>();
               

                using (var db = new DonationDAL())
                {

                    topTenDonate = db.GetTopNumberDonator(5);

                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = topTenDonate
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


        /// <summary>
        /// Get top user who create most thread
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopUserCreateThread()
        {
            try
            {
                List<UserBasicInfoDTO> topTheadCreator;


                using (var db = new UserDAL())
                {
                    topTheadCreator = db.GetTopNumberThreadCreator(5);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = topTheadCreator
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

        [HttpGet]
        public IHttpActionResult GetTopFiveRankUser()
        {
            try
            {
                List<UserBasicInfoDTO> topRankUser;


                using (var db = new UserDAL())
                {
                    topRankUser = db.GetTopNumberRankingUser(5);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = topRankUser
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
    }
}
