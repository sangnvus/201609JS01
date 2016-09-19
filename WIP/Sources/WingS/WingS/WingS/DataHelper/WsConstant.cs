﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.DataHelper
{
    public class WsConstant
    {
        // Crate some constant to use in controller and view
        public const string ConnectionString = "Ws_DataContext";
        public static class PageTitle
        {
            public static string Home = "Hãy chia sẻ việc từ thiện với chúng tôi";
            public static string CreateEvent = "Khởi tạo một event";
        }
        public enum Level { Diamon=2000,Golden = 1000, Silver = 500, Bronze= 200, Newbie = 0 };
    }
}