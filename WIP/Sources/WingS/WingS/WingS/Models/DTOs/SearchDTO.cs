using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class SearchUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public string Phone { get; set; }
        public string FacebookUrl { get; set; }

        public SearchUser()
        {
            UserID = 0;
            UserName = "";
            FullName = "";
            ProfileImage = "";
            UserAddress = "";
            Phone = "";
            FacebookUrl = "";
        }

        public SearchUser(int id, string name, string fullname, string image, string address, string phone,
            string faceurl)
        {
            UserID = id;
            UserName = name;
            FullName = fullname;
            ProfileImage = image;
            UserAddress = address;
            Phone = phone;
            FacebookUrl = faceurl;
        }
    }
}