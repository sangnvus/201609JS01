using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class StatisticManageBasicInforDTO
    {
        public int NumberActiveUser { get; set; }
        public int NumberNotActiveUser { get; set; }
        public int NumberVerifyUser { get; set; }
        public int NumberNotVerifyUser { get; set; }
        public int NumberNewCreateUser { get; set; }
        public int NumberTotalUser { get; set; }
        public int NumberTotalOrganization { get; set; }
        public int NumberTotalEvent { get; set; }
        public int NumberTotalThread { get; set; }

        public StatisticManageBasicInforDTO()
        {
            NumberActiveUser = 0;
            NumberNotActiveUser = 0;
            NumberVerifyUser = 0;
            NumberNotVerifyUser = 0;
            NumberNewCreateUser = 0;
            NumberTotalUser = 0;
            NumberTotalOrganization = 0;
            NumberTotalEvent = 0;
            NumberTotalThread = 0;

        }
    }
}