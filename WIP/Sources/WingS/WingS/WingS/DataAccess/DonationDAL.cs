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
                using (var db = new Ws_DataContext())
                {
                    donation = db.Donations.FirstOrDefault(x => x.DonationId == donationId);
                }

                using (var db = new EventDAL())
                {
                    currentDonation.EventBasicInformation = db.GetEventBasicInfoById(donation.EventId);
                }

                currentDonation.DonationId = donation.DonationId;
                currentDonation.UserId = donation.UserId;
                currentDonation.EventId = donation.EventId;
                currentDonation.DonatedMoney = donation.DonatedMoney;
                currentDonation.DonatedDate = donation.DonatedDate.ToString("hh:mm:ss dd/MM/yy");
                currentDonation.IsPublic = donation.IsPublic;
            }
            catch (Exception)
            {
                //throw;
            }
            return currentDonation;
        }
        public void Dispose()
        {
            
        }
    }
}