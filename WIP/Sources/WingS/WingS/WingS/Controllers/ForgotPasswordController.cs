﻿using System;
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
        /// <returns>home page or error page</returns>
        [HttpPost]
        public ActionResult Index(string userNameOrEmail)
        {
            try
            {
                //xu li doi password
                Random rnd = new Random();
                string userName = string.Empty;
                string userEmail = string.Empty;
                string userNewPass = string.Empty;
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameOrEmail(userNameOrEmail);
                    userName = AccountInfo.UserName;
                    userEmail = AccountInfo.Email;
                    var md5pass = rnd.Next(00000001,99999999).ToString();
                    userNewPass = md5pass;
                    AccountInfo.UserPassword = MD5Helper.MD5Encrypt(md5pass);
                    userDal.UpdateUser(AccountInfo);
                }
                //khai bao bien
                var fromAddress = new MailAddress(WsConstant.ForgotPass.AdminEmail, WsConstant.ForgotPass.WsOrganization);
                var toAddress = new MailAddress(userEmail, userName);
                string fromPassword = WsConstant.ForgotPass.AdminEmailPass;
                string subject = WsConstant.ForgotPass.EmailSubject;
                string body = WsConstant.ForgotPass.EmailContentFirst + "   Tài khoản : " + userName + "\n   Mật khẩu : " +
                              userNewPass + WsConstant.ForgotPass.EmailContentLast;
                //xu li gui mail
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 30000,
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
                TempData["AlertMessage"] = WsConstant.ForgotPass.SentAlert;
                return Redirect("/#/Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("SendMailFailed", "Email can't be send!");
                return Redirect("/#/Error");
            }
        }

        /// <summary>
        /// thuc hien check input
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAdress"></param>
        /// <returns>true and false</returns>
        public JsonResult ValidateUser(string userNameOrEmail)
        {
            Ws_User validAcc = null;
                using (var userDal = new UserDAL())
                {
                    validAcc = userDal.GetUserByUserNameOrEmail(userNameOrEmail);
                }
            if (validAcc != null) return Json(true, JsonRequestBehavior.AllowGet);
            else return Json(false, JsonRequestBehavior.AllowGet);
        }
	}
}