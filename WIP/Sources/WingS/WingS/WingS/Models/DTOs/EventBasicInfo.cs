using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class EventBasicInfo
    {
        public int EventID { get; set; }
        public int CreatorID { get; set; }
        public string CreatorName { get; set; }
        public string EventName { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string VideoUrl { get; set; }
        public bool Status { get; set; }
        public double ExpectedMoney { get; set; }
        public string EventType { get; set; }
        public string CreatedDate { get; set; }
        public string Start_Date { get; set; }
        public string Finish_Date { get; set; }
        public EventBasicInfo()
        {
            CreatorName = "";
            EventType = "";
            ExpectedMoney = 0;
            VideoUrl = "";
            CreatedDate = "";
            EventID = 0;
            CreatorID = 0;
            EventName = "";
            Content = "";
            ImageUrl = "";
            Status = false;
        }
    }
}