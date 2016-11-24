using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class AdminUserListController : ApiController
    {
        // Get list thread by create date
        [HttpGet]
        [ActionName("GetAllUser")]
        public IHttpActionResult GetAllUser()
        {
            List<UserBasicInfoDTO> listUser = new List<UserBasicInfoDTO>();
            try
            {
                using (var db = new UserDAL())
                {
                    listUser = db.GetAllUser();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listUser });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
        }

        [HttpGet]
        [ActionName("ChangeStatusUser")]
        public IHttpActionResult ChangeStatusUser(int userid)
        {
            try
            {
                bool statusUser;
                string userEmail;
                string userName;
                using (var userDal = new UserDAL())
                {
                    var user = userDal.GetUserById(userid);
                    user.IsActive = !user.IsActive;
                    statusUser = user.IsActive;
                    userEmail = user.Email;
                    userName = user.UserName;
                    userDal.UpdateUser(user);
                }
                if (statusUser)
                {
                    //khai bao bien
                    var fromAddress = new MailAddress(WsConstant.ChangeStatusUser.AdminEmail, WsConstant.ChangeStatusUser.WsOrganization);
                    var toAddress = new MailAddress(userEmail, userName);
                    string fromPassword = WsConstant.ChangeStatusUser.AdminEmailPass;
                    string subject = WsConstant.ChangeStatusUser.EmailSubjectUnban;
                    string body = WsConstant.ChangeStatusUser.EmailContentFirst + "   Tài khoản : " + userName + " " + WsConstant.ChangeStatusUser.EmailSubjectUnban;
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
                }
                else
                {
                    //khai bao bien
                    var fromAddress = new MailAddress(WsConstant.ChangeStatusUser.AdminEmail, WsConstant.ChangeStatusUser.WsOrganization);
                    var toAddress = new MailAddress(userEmail, userName);
                    string fromPassword = WsConstant.ChangeStatusUser.AdminEmailPass;
                    string subject = WsConstant.ChangeStatusUser.EmailSubjectBan;
                    string body = WsConstant.ChangeStatusUser.EmailContentFirst + "   Tài khoản : " + userName + " " + WsConstant.ChangeStatusUser.EmailSubjectBan;
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
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = statusUser });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
    }
}
