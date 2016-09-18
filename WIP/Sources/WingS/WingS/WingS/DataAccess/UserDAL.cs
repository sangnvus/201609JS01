using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;

namespace WingS.DataAccess
{
    public class UserDAL
    {
        public static Ws_User GetUserByUserNameOrEmail (string UsernameOrEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.Include("User_Information").FirstOrDefault(x => x.UserName == UsernameOrEmail || x.Email == UsernameOrEmail);
                return User;
            }
        }
        public static Ws_User GetUserByUserNameAndPassword(string UsernameOrEmail, string Password)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.Include("User_Information").FirstOrDefault(x => ((x.UserName == UsernameOrEmail || x.Email == UsernameOrEmail) && x.UserPassword == Password));
                return User;
            }
        }
    }
}