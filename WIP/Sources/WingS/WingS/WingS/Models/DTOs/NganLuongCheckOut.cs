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
        public string numberMoney { get; set; }
        public string inputMoney { get; set; }
        public string isPublic { get; set; }
        public NganLuongCheckOut()
        {
            option_payment = "";
            bankcode = "";
            numberMoney = "";
            inputMoney = "";
            isPublic = "";
        }
    }
}