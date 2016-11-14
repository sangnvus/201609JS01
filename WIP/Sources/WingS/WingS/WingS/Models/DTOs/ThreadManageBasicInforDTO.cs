using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ThreadManageBasicInforDTO
    {
        public int NumberBanThread { get; set; }
        public int NumberNewThread { get; set; }
        public int NumberTotalthread { get; set; }

        public ThreadManageBasicInforDTO()
        {
            NumberBanThread = 0;
            NumberNewThread = 0;
            NumberTotalthread = 0;
        }
    }
}