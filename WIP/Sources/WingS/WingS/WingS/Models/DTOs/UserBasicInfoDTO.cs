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
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int NumberOfPost { get; set; }
        public string DOB { get; set; }
        public string Country { get; set; }
        public string CreateDate { get; set; }
        public int Point { get; set; }
        public int NumberEventDonatedIn { get; set; }
        public decimal TotalMoneyDonatedIn { get; set; }


        public UserBasicInfoDTO()
        {
            UserId = 0;
            UserName = "";
            AccountType = false;
            IsActive = false;
            IsVerify = false;
            FullName = "";
            ProfileImage = "";
            Email = "";
            Gender = "";
            Phone = "";
            Address = "";
            NumberOfPost = 0;
            DOB = "";
            Country = "";
            CreateDate = "";
            Point = 0;
            NumberEventDonatedIn = 0;
            TotalMoneyDonatedIn = 0;
        }
    }
}