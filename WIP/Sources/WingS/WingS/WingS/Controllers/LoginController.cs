using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class LoginController : ModelAccess
    {
        public ActionResult Login()
        {
            // Check if logged
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                FormsAuthentication.SignOut();
                var limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    var cookieName = Request.Cookies[i].Name;
                    var cookie = new HttpCookie(cookieName) { Expires = DateTime.UtcNow.AddDays(-1) };
                    Response.Cookies.Add(cookie);
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginInfoDTO account)
        {
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(account.UserName, account.PassWord);
                if (AccountInfo == null)
                {
                    ModelState.AddModelError("", "Sai mật khẩu hoặc tài khoản không tồn tại!");
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa hoặc chưa xác nhận Email!");
                }
                //Updated Logged time for account.
                else
                {
                    DateTime LastLogin = DateTime.Now;
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
    }
}