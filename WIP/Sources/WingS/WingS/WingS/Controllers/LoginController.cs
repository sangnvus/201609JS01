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

    }
}