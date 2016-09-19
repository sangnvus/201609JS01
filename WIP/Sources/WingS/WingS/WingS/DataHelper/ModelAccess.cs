using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataHelper
{
    public class ModelAccess : Controller
    {
        protected  Ws_User GetCurrentUser()
        {
            Ws_User CurrentUser = null;
            if (User.Identity.Name != null)
            {
                using (var db = new UserDAL())
                {
                    CurrentUser = db.GetUserByUserNameOrEmail(User.Identity.Name);
                    //ViewBag.CurrentUser = new UserBasicInfoDTO()
                    ////Set ViewBag for View
                    //{
                    //    FullName = CurrentUser.User_Information.FullName,
                    //    ProfileImage = CurrentUser.User_Information.ProfileImage,
                    //    AccountType = CurrentUser.AccountType,
                    //    IsActive = CurrentUser.IsActive,
                    //    UserName = CurrentUser.UserName
                    //};

                }
            }
            return CurrentUser;
        }
    }
}