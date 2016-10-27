using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Models.DTOs
{
    public class CreateEventInfo
    {
        public int CreatorID { get; set; }
        public int EventType { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public string ShortDescription { get; set; }
        public double ExpectedMoney { get; set; }
        [AllowHtml]
        public string Contact { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
       
        public string VideoUrl { get; set; }

        public CreateEventInfo()
        {
            CreatorID = 0;
            EventType = 0;
            EventName = "";
            Location = "";
            Content = "";
            StartDate = "";
            FinishDate = "";
            VideoUrl = "";
        }
    }
}