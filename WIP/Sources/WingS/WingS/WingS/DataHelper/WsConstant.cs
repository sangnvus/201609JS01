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
            public static readonly string EmailSubject = "Thông tin tài khoản WingS";
            public static readonly string EmailContentFirst = "Chào bạn, \n\nChúng tôi đã nhận được yêu cầu quên mật khẩu từ bạn\n\nĐây là thông tin về tài khoản hiện tại của bạn:\n\n";
            public static readonly string EmailContentLast = "\n\nBạn có thể đổi mật khẩu tại mục Quản lí tài khoản sau khi đăng nhập\n------\nThân,\nThe WingS Team.";
            public static readonly string SentAlert = "Một email đã được gửi tới địa chỉ email đăng kí của bạn!";
            public static readonly string AdminEmail = "anhtuanck93@gmail.com";
            public static readonly string AdminEmailPass = "tuan1993";
        }
    }
}