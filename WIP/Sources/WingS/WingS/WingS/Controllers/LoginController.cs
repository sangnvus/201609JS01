using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(account.UserName, account.PassWord);
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
                }
                //Add remember me
                if (AccountInfo != null && account.RememberMe)
                {
                    FormsAuthentication.SetAuthCookie(AccountInfo.UserName, account.RememberMe);
                } else if(AccountInfo != null && !account.RememberMe)
                {
                    FormsAuthentication.SetAuthCookie(AccountInfo.UserName, !account.RememberMe);
                }
                
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Logout()
        {   //Set logout Authentication
            FormsAuthentication.SignOut();
            //Clear Cookies
            return RedirectToAction("Index", "Home");
        }
        //Create New Account
        public ActionResult Register(UserRegisterDTO account)
        {

            var Md5pass = MD5Helper.MD5Encrypt(account.PassWord);
            var newUser = new Ws_User
            {
                Email = account.Email,
                UserName = account.UserName,
                UserPassword = Md5pass,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsVerify = true,
                AccountType = false,
                VerifyCode = string.Empty,
                User_Information = new User_Information
                {
                    FullName = account.FullName,
                }
            };
            using (var userDal = new UserDAL())
            {
                userDal.AddNewUser(newUser);
            }


            return RedirectToAction("Index", "Home");
        }
       public JsonResult CheckExistedUserNameOrEmail(string UserNameOrEmail)
        {
            string message = "";
            using (var userDal = new UserDAL())
            {
                var User = userDal.GetUserByUserNameOrEmail(UserNameOrEmail);
                if (User != null)
                {
                    message = "ExistedUserNameOrEmail";
                }
                else message = "ValidUserNameOrEmail";
            }
            return this.Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidateUser(string UserName, string PassWord)
        {
            string messageError = "";
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(UserName, PassWord);
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
                return Redirect("/#/error");
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
    }

}
