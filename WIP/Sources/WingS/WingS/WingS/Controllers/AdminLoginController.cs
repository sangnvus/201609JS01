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
    public class AdminLoginController : ModelAccess
    {
        [HttpPost]
        public ActionResult AdminLogin(LoginInfoDTO account)
        {
            string encryptpass = MD5Helper.MD5Encrypt(account.PassWord);
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndPassword(account.UserName, encryptpass);
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
                    FormsAuthentication.SetAuthCookie(AccountInfo.UserName, !account.RememberMe);
                }
                //Add remember me


                return RedirectToAction("DashBoard", "Admin");
            }
        }
	}
}