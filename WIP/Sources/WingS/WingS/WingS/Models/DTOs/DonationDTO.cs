using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class DonationDTO
    {
        public int DonationId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string TradeCode { get; set; }
        public decimal DonatedMoney { get; set; }
        public string DonatedDate { get; set; }
        public bool IsPublic { get; set; }
        public EventBasicInfo EventBasicInformation { get; set; }

        public DonationDTO()
        {
            DonationId = 0;
            UserId = 0;
            EventId = 0;
            TradeCode = "";
            DonatedMoney = 0;
            DonatedDate = "";
            IsPublic = false;
            EventBasicInformation = new EventBasicInfo();
            
        }
    }
}