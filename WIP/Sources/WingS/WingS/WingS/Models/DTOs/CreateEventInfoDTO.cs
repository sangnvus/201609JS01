using System;
using System.Collections.Generic;
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
        public double ExpectedMoney { get; set; }
        [AllowHtml]
        public string Contact { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string FromDate1 { get; set; }
        public string ToDate1 { get; set; }
        public string Description1 { get; set; }
        public string FromDate2 { get; set; }
        public string ToDate2 { get; set; }
        public string Description2 { get; set; }
        public string FromDate3 { get; set; }
        public string ToDate3 { get; set; }
        public string Description3 { get; set; }
        public string FromDate4 { get; set; }
        public string ToDate4 { get; set; }
        public string Description4 { get; set; }
        public string FromDate5 { get; set; }
        public string ToDate5 { get; set; }
        public string Description5 { get; set; }
        public string FromDate6 { get; set; }
        public string ToDate6 { get; set; }
        public string Description6 { get; set; }
        public string FromDate7 { get; set; }
        public string ToDate7 { get; set; }
        public string Description7 { get; set; }
        public string FromDate8 { get; set; }
        public string ToDate8 { get; set; }
        public string Description8 { get; set; }
        public string FromDate9 { get; set; }
        public string ToDate9 { get; set; }
        public string Description9 { get; set; }
        public string FromDate10 { get; set; }
        public string ToDate10 { get; set; }
        public string Description10 { get; set; }
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
            FromDate10 = "";
            ToDate10 = "";
            Description10 = "";
            FromDate1 = "";
            ToDate1 = "";
            Description1 = "";
            FromDate2 = "";
            ToDate2 = "";
            Description2 = "";
            FromDate3 = "";
            ToDate3 = "";
            Description3 = "";
            FromDate4 = "";
            ToDate4 = "";
            Description4 = "";
            FromDate5 = "";
            ToDate5 = "";
            Description5 = "";
            FromDate6 = "";
            ToDate6 = "";
            Description6 = "";
            FromDate7 = "";
            ToDate7 = "";
            Description7 = "";
            FromDate8 = "";
            ToDate8 = "";
            Description8 = "";
            FromDate9 = "";
            ToDate9 = "";
            Description9 = "";
        }
    }
}