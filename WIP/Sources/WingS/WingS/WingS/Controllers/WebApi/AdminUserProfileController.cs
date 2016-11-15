using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class AdminUserProfileController : ApiController
    {
        /// <summary>
        /// Get user profile using id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get donation information of user using Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GeUsertDonationInformation(int userId)
        {
            try
            {
                List<DonationDTO> userDonationInfor = new List<DonationDTO>();
                List<int> donationIdList = new List<int>();

                using (var db = new Ws_DataContext())
                {
                    donationIdList = db.Donations.Where(x => x.UserId == userId).Select(x => x.DonationId).ToList();
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

        /// <summary>
        /// Get threads that have been created by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCreatedThreadOfUser(int userId)
        {
            try
            {
                List<ThreadBasicInfo> userCreatedThread;

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
    }
}
