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
        public static class ReportUser
        {
            public static readonly string BAD_RULE = "Người dùng này vi phạm nội qui";
            public static readonly string BAD_POST = "Người dùng này có bài viết xấu";
            public static readonly string BAD_CONTENT = "Người dùng này có một số nội dung không phù hợp";
            public static readonly string OTHER = "Khác";
        }
        public static class ReportOrg
        {
            public static readonly string BAD_RULE = "Tổ chức này vi phạm nội qui";
            public static readonly string BAD_EVENT = "Tổ chức này có những sự kiện không hợp lệ";
            public static readonly string BAD_CONTENT = "Tổ chức này có những sự kiện mang tính lừa đảo";
            public static readonly string BAD_ACTION = "Tổ chức này có những hành xử không đúng";
            public static readonly string OTHER = "Khác";
        }
        public static class ReportEvent
        {
            public static readonly string BAD_RULE = "Sự kiện này vi phạm nội qui";
            public static readonly string BAD_EVENT = "Sự kiện này có nội dung xấu";
            public static readonly string BAD_CONTENT = "Tính chất của sự kiện không hợp lệ";
            public static readonly string BAD_ACTION = "Sự kiện này không rõ ràng";
            public static readonly string OTHER = "Khác";
        }
        public static class ReportThread
        {
            public static readonly string BAD_RULE = "Bài viết này vi phạm nội qui";
            public static readonly string BAD_THREAD = "Bài viết này có nội dung xấu";
            public static readonly string BAD_CONTENT = "Tính chất của Bài viết không hợp lệ";
            public static readonly string BAD_ACTION = "Bài viết này không rõ ràng";
            public static readonly string OTHER = "Khác";
        }

        public static class ReportType
        {
            public static readonly string REPORT_USER = "Ws_User";
            public static readonly string REPORT_EVENT = "Events";
            public static readonly string REPORT_THREAD = "Threads";
            public static readonly string REPORT_ORGANAZATION = "Organizations";
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
        public static class ChangeStatusUser
        {
            public static readonly string WsOrganization = "WingS Organization";
            public static readonly string EmailSubjectBan = "Thông báo khóa tài khoản WingS";
            public static readonly string EmailSubjectUnban = "Thông báo mở khóa tài khoản WingS";
            public static readonly string EmailContentFirst = "Chào bạn, \n\n";
            public static readonly string EmailContentUnban = "của bạn hiện đã được mở khóa.Giờ bạn có thể đăng nhập bình thường.\n------\nThân,\nThe WingS Team.";
            public static readonly string EmailContentBan = "của bạn hiện đã bị khóa.Hiện tại bạn không thể đăng nhập bằng tài khoản này.\nNếu có bất kì thắc mắc nào xin hãy liên hệ với ban quản trị.\n------\nThân,\nThe WingS Team.";
            public static readonly string SentAlert = "Một email đã được gửi tới địa chỉ email đăng kí của bạn!";
            public static readonly string SendMailFailed = "Gửi email thất bại!";
            public static readonly string AdminEmail = "WingSFPT@gmail.com";
            public static readonly string AdminEmailPass = "wing@123";
        }

        public static class OrganizationRegistration
        {
            public static readonly string WsAdmin = "WingS Admin";
            public static readonly string EmailSubjectRejectRegistration = "Thông báo hủy bỏ yêu cầu tạo tổ chức tại WingS";
            public static readonly string EmailContentFirst = "Chào bạn,\n\n";
            public static readonly string EmailContentRejectRegistration = "của bạn đã bị từ trối bởi Admin của WingS do không đáp ứng được yêu cầu của việc tạo tổ chức của WingS.\nNếu có bất kì thắc mắc nào xin hãy liên hệ với ban quản trị.\n------\nThân,\nThe WingS Team.";
            public static readonly string SentAlert = "Một email đã được gửi tới địa chỉ email đăng kí của bạn!";
            public static readonly string SendMailFailed = "Gửi email thất bại!";
            public static readonly string AdminEmail = "WingSFPT@gmail.com";
            public static readonly string AdminEmailPass = "wing@123";
        }
        public static class NganLuongApi
        {
            public static readonly string MerchantId = "48283";
            public static readonly string Password = "12ba1130cf119352596dc8e1ba8e5fbf";
            public static readonly string AdminEmail = "WingSFPT@gmail.com";

        }
    }
}