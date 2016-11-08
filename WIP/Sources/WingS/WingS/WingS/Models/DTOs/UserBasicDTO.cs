using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class UserBasicDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool AccountType { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerify { get; set; }
        public string CreatedDate { get; set; }
        public string Email { get; set; }
        public UserBasicDTO()
        {
            UserId = 0;
            UserName = "";
            AccountType = false;
            IsActive = false;
            IsVerify = false;
            Email = "";
        }
    }
}