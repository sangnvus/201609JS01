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
        [HttpGet]
        public IHttpActionResult GeUsertDonationInformation(string userName)
        {
            try
            {
                List<DonationDTO> userDonationInfor = new List<DonationDTO>();
                List<int> donationIdList = new List<int>();

                using (var db = new Ws_DataContext())
                {
                    var user = db.Ws_User.FirstOrDefault(x => x.UserName == userName);
                    if (user != null)
                    {
                        int userId = user.UserID;
                        donationIdList = db.Donations.Where(x => x.UserId == userId).Select(x => x.DonationId).ToList();
                    }
                }

                foreach (int donationId in donationIdList)
                {
                    DonationDTO donation;
                    using (var db = new DonationDAL())
                    {
                        donation = db.GetFullInformationOfDonation(donationId);
                    }

                    userDonationInfor.Add(donation);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get User Profile Successfully",
                    Type = "",
                    Data = userDonationInfor
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get User Donation Infomation!",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult GetCreatedThreadOfUser(string userName)
        {
            try
            {
                List<ThreadBasicInfo> userCreatedThread;

                int userId = 0; 
                using (var db = new Ws_DataContext())
                {
                     userId= db.Ws_User.Where(x => x.UserName == userName).SingleOrDefault().UserID;
                }
                using (var db = new ThreadDAL())
                {
                    userCreatedThread = db.GetThreadsOfUser(userId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get User Profile Successfully",
                    Type = "",
                    Data = userCreatedThread
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get User Donation Infomation!",
                    Type = ""
                });
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateUserInfo(UserBasicInfoDTO UpdateUser)
        {
        
            //Call to accesslayer
            using (var db = new UserDAL())
            {
               var result= db.UpdateUserInfo(UpdateUser, User.Identity.Name);
                if(result)
                {
                   return Ok(new HTTPMessageDTO
                    {
                        Status = WsConstant.HttpMessageType.SUCCESS,
                    });
                }
                else return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                });
            }
           
        }
    }

}
