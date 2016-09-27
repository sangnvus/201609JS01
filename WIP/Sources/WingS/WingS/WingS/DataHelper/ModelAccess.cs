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

                }
            }
         
            return CurrentUser;
        }
    }
}