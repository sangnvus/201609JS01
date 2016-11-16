using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class OrganizationBasicInfo
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string OrganizationName { get; set; }
        public string Introduction { get; set; }
        public string LogoUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerify { get; set; }
        public int Point { get; set; }
        public string CurrentRank { get; set; }

        public OrganizationBasicInfo()
        {
            OrganizationId = 0;
            UserId = 0;
            OrganizationName = "";
            Introduction = "";
            LogoUrl = "";
            Phone = "";
            Email = "";
            Address = "";
            IsActive = false;
            Point = 0;
        }
    }
}