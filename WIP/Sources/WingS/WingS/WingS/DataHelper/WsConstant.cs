using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.DataHelper
{
    public class WsConstant
    {
        public static string randomString()
        {
            string randomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            Random rnd = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = randomString[rnd.Next(randomString.Length)];
            }

            return new String(stringChars);
        }
        // Crate some constant to use in controller and view
        public const string ConnectionString = "Ws_DataContext";

        public static class PageTitle
        {
            public static string Home = "Hãy chia sẻ việc từ thiện với chúng tôi";
            public static string CreateEvent = "Khởi tạo một event";
        }

        public static class Level
        {
            public static readonly int Diamon = 10000;
            public static readonly int Plantium = 5000;
            public static readonly int Golden = 2000;
            public static readonly int Silver = 500;
            public static readonly int Bronze = 200;
            public static readonly int New = 0;
        }


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
            public static readonly string SendMailFailed = "Gửi email thất bại!";
            public static readonly string AdminEmail = "WingSFPT@gmail.com";
            public static readonly string AdminEmailPass = "wing@123";
        }
        public static class VerifyEmail
        {
            public static readonly string WsOrganization = "WingS Organization";
            public static readonly string EmailSubject = "Xác nhận tài khoản đăng ký";
            public static readonly string EmailContentFirst = "Xin chào bạn, \n\nCảm ơn bạn đã đăng ký sử dụng trang web của chúng tôi!" +
                                                              "\nBạn vui lòng nhập đoạn mã sau đây để xác nhận tài khoản đã đăng ký:\n\n";
            public static readonly string EmailContentLast = "\nLink nhập mã xác nhận: http://localhost:2710/#/VerifyAccount \n\nNếu có vấn đề gì xin hãy liên hệ với chúng tôi qua đường link 'https://wings.com.vn'\n------\nYours,\nThe WingS Team.";
            public static readonly string AdminEmail = "WingSFPT@gmail.com";
            public static readonly string AdminEmailPass = "wing@123";
        }
    }
}