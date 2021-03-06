﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class DonationDAL: IDisposable
    {
        /// <summary>
        /// Get Number event which user donate in 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>int</returns>
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
                return 0;
                //throw;
            }

            return numberEvent;
        }

        /// <summary>
        /// Get total money which user has donated.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>decimal</returns>
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
                return 0;
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
                    UserBasicInfoDTO userBasic;
                    using (var db = new UserDAL())
                    {
                        userBasic = db.GetFullInforOfUserAsBasicUser(userId);
                    }
                    listUserDonate.Add(userBasic);
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

        /// <summary>
        /// Get top recently donted user
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<DonationDTO> GetTopRecentlyDonator(int top)
        {
            List<DonationDTO> recentDonation = new List<DonationDTO>();

            try
            {
                List<int> donationIdList;
                using (var db = new Ws_DataContext())
                {
                    donationIdList = db.Donations.OrderByDescending(x => x.DonatedDate).Select(x => x.DonationId).Take(top).ToList();
                }

                foreach (var donationId in donationIdList)
                {
                    DonationDTO donation = GetFullInformationOfDonation(donationId);
                    recentDonation.Add(donation);
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return recentDonation;
        }

        /// <summary>
        /// Get donation information that has been donated by an user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Donation</returns>
        public Donation GetLastDonateInformation(int userId)
        {
            try
            {
                Donation donationInfor;
                using (var db = new Ws_DataContext())
                {
                    donationInfor = db.Donations.OrderByDescending(x=>x.DonatedDate).FirstOrDefault(x => x.UserId == userId);
                }

                return donationInfor;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


        //public List<DonationDAL> GetDonationHistoryOfUser(int userId)
        //{
            
        //}

        public DonationDTO GetFullInformationOfDonation(int donationId)
        {
            DonationDTO currentDonation = new DonationDTO();
            try
            {
                Donation donation;
                string userName = "";
                string userImageUrl = "";
                using (var db = new Ws_DataContext())
                {
                    donation = db.Donations.FirstOrDefault(x => x.DonationId == donationId);
                    var wsUser = db.Ws_User.FirstOrDefault(x => x.UserID == donation.UserId);
                    if (wsUser != null)
                    {
                        userName = wsUser.UserName;
                    }

                    var userInformation = db.User_Information.FirstOrDefault(x => x.UserID == donation.UserId);
                    if (userInformation != null)
                        userImageUrl = userInformation.ProfileImage;
                }

                using (var db = new EventDAL())
                {
                    currentDonation.EventBasicInformation = db.GetFullEventBasicInformation(donation.EventId);
                }

                currentDonation.DonationId = donation.DonationId;
                currentDonation.UserId = donation.UserId;
                currentDonation.UserName = userName;
                currentDonation.UserImageUrl = userImageUrl;
                currentDonation.EventId = donation.EventId;
                currentDonation.TradeCode = donation.TradeCode;
                currentDonation.DonatedMoney = donation.DonatedMoney;
                currentDonation.Content = donation.Content;
                currentDonation.DonatedDate = donation.DonatedDate.ToString("hh:mm:ss dd/MM/yy");
                currentDonation.IsPublic = donation.IsPublic;
            }
            catch (Exception)
            {
                //throw;
            }
            return currentDonation;
        }
        public bool AddNewDonation(DonationDTO donationInfo)
        {
            using (var db = new Ws_DataContext())
            {
                var newDonate = db.Donations.Create();
                newDonate.UserId = donationInfo.UserId;
                newDonate.EventId = donationInfo.EventId;
                newDonate.TradeCode = donationInfo.TradeCode;
                newDonate.DonatedMoney = donationInfo.DonatedMoney;
                newDonate.Content = donationInfo.Content;
                newDonate.IsPublic = donationInfo.IsPublic;
                newDonate.DonatedDate = DateTime.Now; ;
                db.Donations.Add(newDonate);
                db.SaveChanges();
                return true;
            }

        }

        public List<DonationDTO> GetAllDonation()
        {
            var donationList = new List<DonationDTO>();
            try
            {
                List<int> donationIdList;
                using (var db = new Ws_DataContext())
                {
                    donationIdList = db.Donations.Select(x => x.DonationId).ToList();
                }

                foreach (int donationId in donationIdList)
                {
                    var donationDto = GetFullInformationOfDonation(donationId);
                    donationList.Add(donationDto);
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }

            return donationList;
        } 

        public void Dispose()
        {
            
        }
    }
}