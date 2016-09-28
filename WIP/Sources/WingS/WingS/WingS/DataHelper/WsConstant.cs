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
            public static readonly string WsOrganization = "WingS Organization";
            public static readonly string EmailSubject = "WingS account details";
            public static readonly string EmailContentFirst = "Hi, \nAs per your request we have generated a new password for your account.\nHere is the information we now have for this account:";
            public static readonly string EmailContentLast = "You may change your password in User CP - Security Settings after logging in.\n------\nYours,\nThe WingS Team.";
            public static readonly string SentAlert = "A mail has been sent to your register email address!";
            public static readonly string AdminEmail = "anhtuanck93@gmail.com";
            public static readonly string AdminEmailPass = "tuan1993";
        }
    }
}