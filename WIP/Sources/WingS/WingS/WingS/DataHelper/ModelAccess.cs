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
        public Ws_User GetCurrentUser()
        {
            Ws_User CurrentUser = null;
            if(User.Identity.IsAuthenticated)
            {
                CurrentUser = UserDAL.GetUserByUserNameOrEmail(User.Identity.Name);
                ViewBag.CurrentUser = new UserBasicInfoDTO()
                //Set ViewBag for View
                {
                    FullName = CurrentUser.User_Information.FullName,
                    ProfileImage = CurrentUser.User_Information.ProfileImage,
                    AccounType = CurrentUser.AccountType,
                    IsActive = CurrentUser.IsActive,
                    UserName = CurrentUser.UserName
                };
            }
            return CurrentUser;
        }
    }
}