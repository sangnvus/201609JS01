using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class LoginController : ModelAccess
    {
        [HttpPost]
        public ActionResult Login(LoginInfoDTO account)
        {
            string encryptpass = MD5Helper.MD5Encrypt(account.PassWord);
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(account.UserName, encryptpass,false);
                if (AccountInfo == null)
                {
                    ModelState.AddModelError("WrongPassword", "Sai mật khẩu hoặc tài khoản không tồn tại!");
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    ModelState.AddModelError("LockedAccount", "Tài khoản bị khóa hoặc chưa xác nhận Email!");
                }
                //Updated Logged time for account.
                else
                {
                    AccountInfo.LastLogin = DateTime.Now;
                    userDal.UpdateUser(AccountInfo);
                    FormsAuthentication.SetAuthCookie(AccountInfo.UserName, true);
                }
                //Add remember me
               
                
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Logout()
        {   //Set logout Authentication
            FormsAuthentication.SignOut();
            //Clear Cookies
            return RedirectToAction("Index", "Home");
        }
        //Create New Account and Send Verify code
      
        [HttpPost]
        public ActionResult Register(UserRegisterDTO account)
        {
            try
            {
            //Save data to database
            Random rnd = new Random();
            var verifycode = rnd.Next(999999).ToString();
            var Md5pass = MD5Helper.MD5Encrypt(account.PassWord);
                var newUser = new Ws_User
                {
                    Email = account.Email,
                    UserName = account.UserName,
                    UserPassword = Md5pass,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    IsVerify = false,
                    AccountType = false,
                    VerifyCode = verifycode,
                    User_Information = new User_Information
                    {
                     ProfileImage = "Content/Images/avatar_default.png",
                     FullName = account.FullName,
                     EFullName = ConvertToUnSign.Convert(account.FullName)
                    }
            };
            using (var userDal = new UserDAL())
            {
                userDal.AddNewUser(newUser);
            }

            // Send Verify code
                //khai báo biến để gửi mã xác nhận
                var fromAddress = new MailAddress(WsConstant.VerifyEmail.AdminEmail, WsConstant.VerifyEmail.WsOrganization);
                var toAddress = new MailAddress(account.Email, account.UserName);
                string fromPassword = WsConstant.VerifyEmail.AdminEmailPass;
                string subject = WsConstant.VerifyEmail.EmailSubject;
                string body = WsConstant.VerifyEmail.EmailContentFirst + "  Tên đăng nhập : " + account.UserName + "\n  Mã xác nhận : " +
                              verifycode + WsConstant.VerifyEmail.EmailContentLast;
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
                //chuyển đến trang đăng ký thành công
                return Redirect("/#/RegisterSuccess");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
        }
        // Xác nhận tài khoản: update trường Isverify
        [HttpPost]
        public ActionResult updateIsverify(string UserName)
        {
            try
            {
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameOrEmail(UserName);
                    AccountInfo.IsVerify = true;
                    userDal.UpdateUser(AccountInfo);
                }
                //return RedirectToAction("Login", "Client");
                return Redirect("/#/Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }
        //check valid of verifycode
        public JsonResult CheckVerifyCode(string UserName, string VerifyCode)
        {
            string message = "";
            string connect = "";
            try
            {
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameOrEmail(UserName);
                    if (AccountInfo == null )
                    {
                        message = "NotExistUser";
                    }
                    else if (AccountInfo.IsVerify==true)
                    {
                        message = "IsVerify";
                    }
                    else if (VerifyCode != AccountInfo.VerifyCode)
                    {
                        message = "ErrorCode";
                    }
                   
                    else message = "Success";
                }
                return this.Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                connect = "Exception";
                return this.Json(connect, JsonRequestBehavior.AllowGet);
            }

        }


        //Check Existed UserName or Email
        public JsonResult CheckExistedUserNameOrEmail(string UserName, string Email)
        {
            string message = "";
            using (var userDal = new UserDAL())
            {
                var check = userDal.GetUserByUserNameAndEmail(UserName, Email);
                if (check == 1)
                {
                    message = "ExistedUser";
                }
                else if (check == 2)
                {
                    message = "ExistedEmail";
                }
                else message = "Success";
            }
            return this.Json(message, JsonRequestBehavior.AllowGet);
        }


        //Check UserName and PassWord
        public JsonResult ValidateUser(string UserName, string PassWord, bool AccountType)
        {
            string messageError = "";
            string encryptpass = MD5Helper.MD5Encrypt(PassWord);
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(UserName, encryptpass, AccountType);
                if (AccountInfo == null)
                {
                    messageError = "WrongPass";
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    messageError = "Locked";
                }
                else messageError = "Success";
            }
            return this.Json(messageError,JsonRequestBehavior.AllowGet);
        }
        public ActionResult FacebookCallback(string code)
        {
            try
            {
                var fb = new FacebookClient();
                dynamic result = fb.Post("oauth/access_token", new
                {
                    client_id = "167432310370225",
                    client_secret = "af5a15c4b243b7d4ef5a586a93289dca",
                    redirect_uri = RedirectUri.AbsoluteUri,
                    code = code
                });
                var accessToken = result.access_token;
                // Store the access token in the session
                Session["AccessToken"] = accessToken;
                // update the facebook client with the access token so 
                // we can make requests on behalf of the user
                fb.AccessToken = accessToken;
                // Get the user's information
                dynamic me = fb.Get("/me?fields=id,name,gender,link,birthday,email,bio");
                //dynamic me = fb.Get("/me?fields=id,name,gender,link,birthday,email,bio,website,location");
                string facebookId = me.id;
                string email = me.email;

                if (string.IsNullOrEmpty(email))
                {
                    email = facebookId + "@facebook.com";
                }
                me.email = email;
                // select from DB
                using (var db = new UserDAL()) { 
                    var newUser = db.GetUserByUserNameOrEmail(email);
                    if (newUser == null)
                    {
                        newUser = db.RegisterFacebook(me);
                    }
                    else if (newUser != null && newUser.IsActive == false)
                    {
                    // user is Locked
                    TempData["loginMessageError"] = "Tài khoản của bạn đã bị khóa!";
                    return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        newUser.LastLogin = DateTime.UtcNow;
                        newUser = db.UpdateUser(newUser);

                    }
                    // Set the auth cookie
                    FormsAuthentication.SetAuthCookie(newUser.UserName, true);
                newUser.LastLogin = DateTime.UtcNow;
                db.UpdateUser(newUser);
                }

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult AuthenFacebook()
        {
            try
            {
                var fb = new FacebookClient();
                var loginUrl = fb.GetLoginUrl(new
                {
                    client_id = "167432310370225",
                    client_secret = "af5a15c4b243b7d4ef5a586a93289dca",
                    redirect_uri = RedirectUri.AbsoluteUri,
                    response_type = "code",
                    scope = "email,user_birthday,user_about_me" 
                });
                return Redirect(loginUrl.AbsoluteUri);
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }

        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallback")
                };
                return uriBuilder.Uri;
            }
        }

        // Change Pass
        [HttpPost]
        public ActionResult ChangePass(string userNameChange, string newPass)
        {
            var Md5pass = MD5Helper.MD5Encrypt(newPass);
            try
            {
                using (var userDal = new UserDAL())
                {
                    var AccountInfo = userDal.GetUserByUserNameOrEmail(userNameChange);
                    AccountInfo.UserPassword = Md5pass;
                    userDal.UpdateUser(AccountInfo);
                }
                FormsAuthentication.SignOut();
                return Redirect("/#/Login");
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }
    }

}
