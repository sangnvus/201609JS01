using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class SearchUser
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public string Phone { get; set; }
        public string FacebookUrl { get; set; }

        public SearchUser()
        {
            UserID = 0;
            FullName = "";
            ProfileImage = "";
            UserAddress = "";
            Phone = "";
            FacebookUrl = "";
        }
    }
}