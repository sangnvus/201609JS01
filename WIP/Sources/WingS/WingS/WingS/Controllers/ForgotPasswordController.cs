using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;

namespace WingS.Controllers
{
    public class ForgotPasswordController : ModelAccess
    {
        /// <summary>
        /// thuc hien gui mail
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAdress"></param>
        /// <returns>home page</returns>
        [HttpPost]
        public ActionResult Index(string userName,string emailAdress)
        {
            try
            {
                //xu li doi password
                Random rnd = new Random();
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameAndEmail(userName, emailAdress);
                    AccountInfo.UserPassword = rnd.Next(999999).ToString();
                    userDal.UpdateUser(AccountInfo);
                }
                //khai bao bien
                var fromAddress = new MailAddress("anhtuanck93@gmail.com", "WingS Organization");
                var toAddress = new MailAddress(emailAdress, userName);
                const string fromPassword = "tuan1993";
                const string subject = "Test send mail";
                string body = "Body of the mail la la la la la!!!!"+ rnd;
                //xu li gui mail
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                //hien thong bao success
                TempData["AlertMessage"] = "A mail has been sent to your register email address!";
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// thuc hien check input
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAdress"></param>
        /// <returns>true and false</returns>
        public JsonResult ValidateUser(string userName, string emailAdress)
        {
            string messageError = "";
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndEmail(userName, emailAdress);
                if (AccountInfo == null)
                {
                    messageError = "notExist";
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    messageError = "blocked";
                }
                else
                {
                    messageError = "Success";
                }
            }
            return this.Json(messageError, JsonRequestBehavior.AllowGet);
        }
	}
}