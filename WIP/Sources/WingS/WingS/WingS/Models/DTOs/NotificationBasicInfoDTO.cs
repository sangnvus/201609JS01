using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class NotificationBasicInfoDTO
    {
        public string CreatorName { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string Content { get; set; }
        public string NotifyUrl { get; set; }
    }
}