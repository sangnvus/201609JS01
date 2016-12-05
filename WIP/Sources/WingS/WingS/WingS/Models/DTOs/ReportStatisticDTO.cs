using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ReportStatisticDTO
    {
        public int IsReportedId { get; set; }
        public string IsreportedUserName { get; set; }
        public string IsreportedImage { get; set; }
        public int TotalReportedTimes { get; set; }
        public int NewReportedTimes { get; set; }
        public bool IsreportedUserStatus { get; set; }

    }
}