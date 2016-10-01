using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class OrganizationBasicInfo
    {
        public int OrganazationID { get; set; }
        public string OrganazationName { get; set; }
        public string Introduction { get; set; }
        public string LogoUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public int Point { get; set; }

        public OrganizationBasicInfo()
        {
            OrganazationID = 0;
            OrganazationName = "";
            Introduction = "";
            LogoUrl = "";
            Phone = "";
            Email = "";
            Address = "";
            Status = false;
            Point = 0;
        }
    }
}