using System;
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

        public enum Level
        {
            Diamon = 2000,
            Golden = 1000,
            Silver = 500,
            Bronze = 200,
            Newbie = 0
        };

        public static class HttpMessageType
        {
            public static readonly string NOT_AUTHEN = "not-authen";
            public static readonly string NOT_FOUND = "not-found";
            public static readonly string BAD_REQUEST = "bad-request";
            public static readonly string SUCCESS = "success";
            public static readonly string ERROR = "error";
        }

        public static class ForgotPass
        {
            public static readonly string wsOrganization = "WingS Organization";
            public static readonly string emailSubject = "Test send mail";
            public static readonly string emailContent = "Body of the mail la la la la la!!!!";
            public static readonly string sentAlert = "A mail has been sent to your register email address!";
        }
    }
}