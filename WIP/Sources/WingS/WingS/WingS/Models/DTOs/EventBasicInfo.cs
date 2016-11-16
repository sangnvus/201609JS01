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
        public string CreatorUserName { get; set; }
        public string OrganizationName { get; set; }
        public string CreatorName { get; set; }
        public string EventName { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string ContactInfo { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> ImageAlbum { get; set; }
        public string Location { get; set; }
        public string VideoUrl { get; set; }
        public bool Status { get; set; }
        public string TimeStatus { get; set; }
        public double ExpectedMoney { get; set; }
        public string EventType { get; set; }
        public string CreatedDate { get; set; }
        public string Start_Date { get; set; }
        public string Finish_Date { get; set; }

        public EventBasicInfo()
        {
            EventID = 0;
            CreatorID = 0;
            CreatorUserName = "";
            OrganizationName = "";
            CreatorName = "";
            EventName = "";
            ShortDescription = "";
            Content = "";
            ContactInfo = "";
            MainImageUrl = "";
            ImageAlbum = null;
            Location = "";
            VideoUrl = "";
            Status = false;
            TimeStatus = "";
            ExpectedMoney = 0;
            EventType = "";
            CreatedDate = "";
            Start_Date = "";
            Finish_Date = "";

        }
    }
}