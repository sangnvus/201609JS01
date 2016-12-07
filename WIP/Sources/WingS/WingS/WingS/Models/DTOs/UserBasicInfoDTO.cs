using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class UserBasicInfoDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool AccountType { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerify { get; set; }
        public bool IsOrganazation { get; set; }
        public bool IsOrganazationVerify { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string OrganazationName { get; set; }
        public string JoinedDate { get; set; }
        public string UserSignature { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int NumberOfPost { get; set; }
        public string DOB { get; set; }
        public string Country { get; set; }
        public string FacebookUri { get; set; }
        public string CreateDate { get; set; }
        public int Point { get; set; }
        public string CurrentRank { get; set; }
        public double RankPercent { get; set; }
        public int NumberEventDonatedIn { get; set; }
        public decimal TotalMoneyDonatedIn { get; set; }
        public decimal LastDonateMoney { get; set; }
        public string LastDonateDate { get; set; }
        public int DonatedEventId { get; set; }
        public string DonatedEventName { get; set; }
        public UserBasicInfoDTO()
        {
            UserId = 0;
            UserName = "";
            AccountType = false;
            IsActive = false;
            IsVerify = false;
            FullName = "";
            ProfileImage = "";
            FacebookUri = "";
            Email = "";
            Gender = "";
            Phone = "";
            Address = "";
            NumberOfPost = 0;
            DOB = "";
            Country = "";
            CreateDate = "";
            Point = 0;
            IsOrganazation = false;
            CurrentRank = "";
            OrganazationName = "";
            UserSignature = "";
            RankPercent = 0;
            JoinedDate = "";
            NumberEventDonatedIn = 0;
            TotalMoneyDonatedIn = 0;
            DonatedEventId = 0;
            DonatedEventName = "";
        }
    }
}