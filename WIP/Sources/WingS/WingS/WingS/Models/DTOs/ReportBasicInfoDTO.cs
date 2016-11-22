using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ReportBasicInfoDTO
    {
        public int ReportId { get; set; }
        public string Content { get; set; }
        public string ReportedDate { get; set; }
        public string ReportType { get; set; }
        public int ReportTo { get; set; }
    }
}