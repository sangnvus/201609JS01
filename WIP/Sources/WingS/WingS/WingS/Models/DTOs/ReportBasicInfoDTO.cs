using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ReportBasicInfoDTO
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string ReportorUserName { get; set; }
        public string ReportorImage { get; set; }
        public string Reason { get; set; }
        public string ReportTime { get; set; }
        public string Type { get; set; }
        public int ReportTo { get; set; }
        public string ReportToName { get; set; }
        public string ReportToImage { get; set; }
        public bool Status { get; set; }
        public string UpdatedTime { get; set; }
        //public UserBasicInfoDTO UserMadeReport { get; set; }
        //public UserBasicInfoDTO UserIsReported { get; set; }
        //public EventBasicInfo EventIsReported { get; set; }
        //public string ThreadIsReported { get; set; }
        //public string OrganizationIsReported { get; set; }

    }
}