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
        public string Creator { get; set; }
        public string ThreadName { get; set; }
        public List<String> ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Views { get; set; }
        public bool Status { get; set; }
        public String CreatedDate { get; set; }
        public ThreadBasicInfo()
        {
            ImageUrl = null;
            ThreadID = 0;
            UserID = 0;
            Creator = "";
            ThreadName = "";
            Content = "";
            Likes = 0;
            Comments = 0;
            Views = 0;
            Status = false;
            CreatedDate = "";
        }
    }
}