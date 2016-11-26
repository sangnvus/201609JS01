using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ListDonatorDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public decimal DonatedMoney { get; set; }
        public string Content { get; set; }
        public string DonatedDate { get; set; }
    }
}