using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class ForgotPasswordController : ModelAccess
    {
        [HttpPost]
        public ActionResult Index(string userName,string emailAdress)
        {
            try
            {
                var fromAddress = new MailAddress("anhtuanck93@gmail.com", "WingS");
                var toAddress = new MailAddress(emailAdress, userName);
                const string fromPassword = "tuan1993";
                const string subject = "Test send mail";
                const string body = "Body of the mail la la la la la!!!!";

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
                TempData["AlertMessage"] = "A mail has been sent to your register email address!";
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
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
                else messageError = "Success";
            }
            return this.Json(messageError, JsonRequestBehavior.AllowGet);
        }
	}
}