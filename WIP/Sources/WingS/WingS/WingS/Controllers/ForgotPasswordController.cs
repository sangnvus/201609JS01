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
    public class ForgotPasswordController : ModelAccess
    {
        [HttpPost]
        public ActionResult Index(string userName,string emailAdress)
        {
            using (var userDal = new UserDAL())
            {
                var AccountInfo = userDal.GetUserByUserNameAndEmail(userName, emailAdress);
                if (AccountInfo == null)
                {
                    ModelState.AddModelError("WrongUserNameorPassword", "Tài khoản hoặc email không tồn tại!");
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    ModelState.AddModelError("LockedAccount", "Tài khoản bị khóa hoặc chưa xác nhận Email!");
                }
                else
                {
                    
                }
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
                    messageError = "Tài khoản hoặc email không tồn tại!";
                }
                else if (!AccountInfo.IsActive || !AccountInfo.IsVerify)
                {
                    messageError = "Tài khoản bị khóa hoặc chưa xác nhận Email!";
                }
                else messageError = "Success";
            }
            return this.Json(messageError, JsonRequestBehavior.AllowGet);
        }
	}
}