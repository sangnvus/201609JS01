using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

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
        public Ws_User RegisterFacebook(dynamic me)
        {
            string email = me.email;
            // Create new User
            var newUser = new Ws_User
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                UserPassword = string.Empty,
                IsVerify = true,
                LastLogin = DateTime.UtcNow,
                AccountType = false,
                UserName = "fb." + email.Split(new string[] { "@" }, StringSplitOptions.None)[0],
                VerifyCode = string.Empty,
                User_Information = new User_Information
                {
                    UserAddress = me.location,
                    FullName = me.name,
                    Gender = me.gender,
                    DoB = Convert.ToDateTime(me.birthday),
                    FacebookUrl = me.link,
                    ProfileImage = "https://graph.facebook.com/" + me.id + "/picture?type=large",
                    Country = string.Empty,
                    Phone = string.Empty,
                    OrgnazationIDFollow = string.Empty,
                    UserSignature = string.Empty,
                    Point = 0
                }
            };
            // Facebook account

            // insert user to Database
            newUser = AddNewUser(newUser);

            return newUser;
        }
        public  Ws_User GetUserByUserNameAndPassword(string UsernameOrEmail, string Password)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => ((x.UserName == UsernameOrEmail || x.Email == UsernameOrEmail) && x.UserPassword == Password));
                return User;
            }
        }
        public Ws_User AddNewUser(Ws_User newUser)
        {
            using (var db = new Ws_DataContext())
            {
                db.Ws_User.Add(newUser);
                db.SaveChanges();
                return GetUserByUserNameOrEmail(newUser.Email);
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
        public UserBasicInfoDTO GetUserBasicInfo(string userNameOrEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var currentUser = (from user in db.Ws_User
                                   where user.UserName.Equals(userNameOrEmail) || user.Email.Equals(userNameOrEmail)
                                   select new UserBasicInfoDTO
                                   {
                                       FullName = user.User_Information.FullName,
                                       IsActive = user.IsActive,
                                       ProfileImage = user.User_Information.ProfileImage,
                                       UserName = user.UserName,
                                       AccountType = user.AccountType,
                                   }).FirstOrDefault();
                return currentUser;
            }
        }

        /// <summary>
        /// get data by user name and user email
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userEmail"></param>
        /// <returns>user info</returns>
        public Ws_User GetUserByUserNameAndEmail(string userName,string userEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => x.UserName == userName || x.Email == userEmail);
                return User;
            }
        }
        public void Dispose()
        {
            
        }
    }
}