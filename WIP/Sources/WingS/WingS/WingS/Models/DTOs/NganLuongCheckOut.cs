using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class NganLuongCheckOut
    {
        public string option_payment { get; set; }
        public string bankcode { get; set; }
        public string buyer_fullname { get; set; }
        public string buyer_email { get; set; }
        public string buyer_mobile { get; set; }
        public string numberMoney { get; set; }
        public string inputMoney { get; set; }
        public NganLuongCheckOut()
        {
            option_payment = "";
            bankcode = "";
            buyer_fullname = "";
            buyer_email = "";
            buyer_mobile = "";
            numberMoney = "";
            inputMoney = "";
        }
    }
}