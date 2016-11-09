using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class DonationDAL: IDisposable
    {
        public int GetNumberEventDonatedInByUsingUserId(int userId)
        {
            int numberEvent = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberEvent = db.Donations.Where(x => x.UserId == userId).Select(x => x.EventId).Distinct().Count();
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            return numberEvent;
        }

        public decimal GetTotalMoneyDonatedInByUsingUserId(int userId)
        {
            decimal totalMoney = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    totalMoney = db.Donations.Where(x => x.UserId == userId).Select(x => x.DonatedMoney).Sum();
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return totalMoney;
        }

        /// <summary>
        /// Get top Donator
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<UserBasicInfoDTO> GetTopNumberDonator(int top)
        {
            List<UserBasicInfoDTO> topTenDonator = new List<UserBasicInfoDTO>();

            try
            {
                List<UserBasicInfoDTO> listUserDonate = new List<UserBasicInfoDTO>();
                

                // lay ra nhung userid co trong bang Donation
                List<int> listUserIdInDonation;
                
                using (var db = new Ws_DataContext())
                {
                    var listUserId = db.Donations.Select(x => x.UserId).Distinct();
                    listUserIdInDonation = listUserId.ToList();
                }

                // Lay thong tin cua nhung user ma co id trong Donation
                foreach (int userId in listUserIdInDonation)
                {
                    Ws_User user;
                    UserBasicInfoDTO userBasic;

                    using (var db = new UserDAL())
                    {
                        user = db.GetUserById(userId);
                        userBasic = db.GetUserInfo(user.UserName);

                        userBasic.NumberEventDonatedIn = GetNumberEventDonatedInByUsingUserId(user.UserID);
                        userBasic.TotalMoneyDonatedIn = GetTotalMoneyDonatedInByUsingUserId(user.UserID);
                    }

                    listUserDonate.Add(new UserBasicInfoDTO
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
                        //NumberOfPost = numberOfPost,
                        DOB = userBasic.DOB,
                        Country = userBasic.Country,
                        CreateDate = user.CreatedDate.ToString("H:mm:ss dd/MM/yy"),
                        Point = userBasic.Point,
                        NumberEventDonatedIn = userBasic.NumberEventDonatedIn,
                        TotalMoneyDonatedIn = userBasic.TotalMoneyDonatedIn
                    });
                }
                
                // Lay top 10 user donate nhieu nhat
                topTenDonator = listUserDonate.OrderByDescending(x => x.TotalMoneyDonatedIn).Take(top).ToList();

                
            }
            catch (Exception)
            {
                
                //throw;
            }
            return topTenDonator;
        }

        public List<UserBasicInfoDTO> GetTopNumberThreadCreator(int top)
        {
            List<UserBasicInfoDTO> topThreadCreator = new List<UserBasicInfoDTO>();

            try
            {
                List<UserBasicInfoDTO> listUser = new List<UserBasicInfoDTO>();


                // lay ra nhung userid co trong bang Thread
                List<int> listUserIdInDonation;

                using (var db = new Ws_DataContext())
                {
                    var listUserId = db.Threads.Select(x => x.UserId).Distinct();
                    listUserIdInDonation = listUserId.ToList();
                }

                // Lay thong tin cua nhung user ma co id trong Donation
                foreach (int userId in listUserIdInDonation)
                {
                    UserBasicInfoDTO userBasic;

                    using (var db = new UserDAL())
                    {
                        userBasic = db.GetFullInforOfUserAsBasicUser(userId);
                    }

                    listUser.Add(userBasic);
                }

                // Lay top 10 user donate nhieu nhat
                topThreadCreator = listUser.OrderByDescending(x => x.NumberOfPost).Take(top).ToList();


            }
            catch (Exception)
            {

                //throw;
            }
            return topThreadCreator;
        } 

        public void Dispose()
        {
            
        }
    }
}