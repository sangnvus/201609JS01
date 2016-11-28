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
        /// Get statistic manage basisc information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStatisticManageBasicInfor()
        {
            try
            {
                StatisticManageBasicInforDTO statistic = new StatisticManageBasicInforDTO();
                
                using (var db = new UserDAL())
                {
                    statistic.NumberActiveUser = db.CountUserActiveOrNot(true);
                    statistic.NumberNotActiveUser = db.CountUserActiveOrNot(false);
                    statistic.NumberVerifyUser = db.CountUserVerifyOrNot(true);
                    statistic.NumberNotVerifyUser = db.CountUserVerifyOrNot(false);
                    statistic.NumberNewCreateUser = db.CountNewUser();
                    statistic.NumberTotalUser = db.CountTotalUser();
                    
                    //Get number of event
                    statistic.NumberTotalEvent = db.CountTotalEvent();
                    //Get number of post
                    statistic.NumberTotalThread = db.CountTotalThread();
                }

                using (var db = new OrganizationDAL())
                {
                    //Get number of organization
                    statistic.NumberTotalOrganization = db.CountTotalOrganization();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = statistic
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
                        UserBasicInfoDTO userBasic = db.GetFullInforOfUserAsBasicUser(user.UserID);
                        userListBasic.Add(userBasic);
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
        public IHttpActionResult GetTopRankUser(int top)
        {
            try
            {
                List<UserBasicInfoDTO> topRankUser;


                using (var db = new UserDAL())
                {
                    topRankUser = db.GetTopNumberRankingUser(top);
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

        /// <summary>
        /// Get recently donated user
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopRecentDonator(int top)
        {
            try
            {
                List<DonationDTO> topRecentDonator;


                using (var db = new DonationDAL())
                {

                    topRecentDonator = db.GetTopRecentlyDonator(top);

                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = topRecentDonator
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
