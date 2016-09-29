using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ThreadBasicInfo
    {
        public int ThreadID { get; set; }
        public int UserID { get; set; }
        public string ThreadName { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public ThreadBasicInfo()
        {
            ImageUrl = "";
            ThreadID = 0;
            UserID = 0;
            ThreadName = "";
            Content = "";
            Status = false;
        }
    }
}