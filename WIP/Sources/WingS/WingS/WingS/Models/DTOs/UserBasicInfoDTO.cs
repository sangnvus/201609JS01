using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class UserBasicInfoDTO
    {

        public string UserName { get; set; }
        public bool AccountType { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
    }
}