using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class User_Information
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Country { get; set; }
        public string FacebookUrl { get; set; }
        public string OrgnazationIDFollow { get; set; }
        public string UserSignature { get; set; }
        public int Point { get; set; }
        public virtual WS_User User { get; set; }
    }
}