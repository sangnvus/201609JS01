using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WingS.Models;

namespace WingS.DataAccess
{
    public class UserDAL: IDisposable
    {
        public  Ws_User GetUserByUserNameOrEmail (string UsernameOrEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => x.UserName == UsernameOrEmail || x.Email == UsernameOrEmail);
                return User;
            }
        }
        public  Ws_User GetUserByUserNameAndPassword(string UsernameOrEmail, string Password)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => ((x.UserName == UsernameOrEmail || x.Email == UsernameOrEmail) && x.UserPassword == Password));
                return User;
            }
        }
        public Ws_User UpdateUser(Ws_User User)
        {
            using (var db = new Ws_DataContext())
            {
                db.Ws_User.AddOrUpdate(User);
                db.SaveChanges();

                return GetUserByUserNameOrEmail(User.UserName);
            }
        }
        public void Dispose()
        {
            
        }
    }
}