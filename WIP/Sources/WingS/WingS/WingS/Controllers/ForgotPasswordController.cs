using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;

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
        public ActionResult Index(string userNameOrEmail)
        {
            try
            {
                //xu li doi password
                Random rnd = new Random();
                string userName = string.Empty;
                string userEmail = string.Empty;
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameOrEmail(userName);
                    userName = AccountInfo.UserName;
                    userEmail = AccountInfo.Email;
                    AccountInfo.UserPassword = rnd.Next(999999).ToString();
                    userDal.UpdateUser(AccountInfo);
                }
                //khai bao bien
                    var fromAddress = new MailAddress("anhtuanck93@gmail.com", WsConstant.ForgotPass.wsOrganization);
                    var toAddress = new MailAddress(userEmail, userName);
                    const string fromPassword = "tuan1993";
                    string subject = WsConstant.ForgotPass.emailSubject;
                    string body = WsConstant.ForgotPass.emailContent + rnd;
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
                TempData["AlertMessage"] = WsConstant.ForgotPass.sentAlert;
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
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
            bool validAcc;
                using (var userDal = new UserDAL())
                {
                    validAcc = userDal.GetUserByUserNameAndEmail(userName, emailAdress);
                }
                return this.Json(validAcc, JsonRequestBehavior.AllowGet);
            
        }
	}
}