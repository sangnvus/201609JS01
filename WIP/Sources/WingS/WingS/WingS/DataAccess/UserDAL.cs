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
        public Ws_User GetUserByUserNameOrEmail (string UsernameOrEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => x.UserName.Equals(UsernameOrEmail) || x.Email.Equals(UsernameOrEmail));
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
                    EFullName = ConvertToUnSign.Convert(me.name),
                    Gender = me.gender,
                    DoB = (Convert.ToDateTime(me.birthday)!=DateTime.MinValue)?Convert.ToDateTime(me.birthday):null,
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
        public  Ws_User GetUserByUserNameAndPassword(string UsernameOrEmail, string Password,  bool accountType)
        {
            using (var db = new Ws_DataContext())
            {
                var User = db.Ws_User.FirstOrDefault(x => ((x.UserName.Equals(UsernameOrEmail) || x.Email.Equals(UsernameOrEmail)) && x.UserPassword.Equals(Password) && x.AccountType.Equals(accountType)));
                return User;
            }
        }
        //Get UserName or Email
        public int GetUserByUserNameAndEmail(string Username, string Email)
        {
            using (var db = new Ws_DataContext())
            {
                if(db.Ws_User.FirstOrDefault(x => ( x.Email.Equals(Email)))!=null){
                    return 2;
                }
                else if (db.Ws_User.FirstOrDefault(x => (x.UserName.Equals(Username))) != null)
                {
                    return 1;
                }
                else
                return 0;
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
                                       UserId = user.UserID,
                                       FullName = user.User_Information.FullName,
                                       IsActive = user.IsActive,
                                       ProfileImage = user.User_Information.ProfileImage,
                                       UserName = user.UserName,
                                       AccountType = user.AccountType,                                    
                                 }).FirstOrDefault();
                return currentUser;
            }
        }

        //Get user information (Profile page)
        public UserBasicInfoDTO GetUserInfo(string userNameOrEmail)
        {
            using (var db = new Ws_DataContext())
            {
                var currentUser = (from user in db.Ws_User
                                   where user.UserName.Equals(userNameOrEmail) || user.Email.Equals(userNameOrEmail)
                                   select new UserBasicInfoDTO
                                   {
                                       UserId = user.UserID,
                                       FullName = user.User_Information.FullName,
                                       IsActive = user.IsActive,
                                       ProfileImage = user.User_Information.ProfileImage,
                                       UserName = user.UserName,
                                       AccountType = user.AccountType,
                                       Email = user.Email,
                                       Address = user.User_Information.UserAddress,
                                       Country = user.User_Information.Country,
                                       Gender = user.User_Information.Gender,
                                       Phone = user.User_Information.Phone,
                                       Point = user.User_Information.Point,
                                      // CreateDate = user.CreatedDate.ToString("H:mm:ss dd/MM/yy"),
                                       //DOB = user.User_Information.DoB.ToString("dd/MM/yy"),
     

                                   }).FirstOrDefault();
                return currentUser;
            }
        }

        /// <summary>
        /// Get user using user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ws_User GetUserById(int id)
        {
            using (var db = new Ws_DataContext())
            {
                var currentUser = db.Ws_User.FirstOrDefault(x => x.UserID == id);
                return currentUser;
            }
        }

        /// <summary>
        /// get user infomation using user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User_Information GetUserInformation(int userId)
        {

            using (var db = new Ws_DataContext())
            {
                var currentUserInfo = db.User_Information.FirstOrDefault(x => x.UserID == userId);
                return currentUserInfo;
            }
        }
        /// <summary>
        /// Get All User in WS_USER
        /// </summary>
        /// <returns>list ws_user</returns>
        public List<Ws_User> GetAllUser()
        {
            using (var db = new Ws_DataContext())
            {
                var allUser = (from row in db.Ws_User where row.AccountType == false select row).ToList();
                return allUser;
            }
        }
        /// <summary>
        /// Count number of user is actived or not
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public int CountUserActiveOrNot(bool isActive)
        {
            using (var db = new Ws_DataContext())
            {
                int numberUser;
                if (isActive)
                {
                    numberUser = db.Ws_User.Count(x => x.IsActive == isActive && x.IsVerify == isActive && x.AccountType == false);
                }
                else
                {
                    numberUser = db.Ws_User.Count(x => x.IsActive == isActive && x.AccountType == false);
                }
                return numberUser;
            }
        }

        /// <summary>
        /// Count number of user is verified or not
        /// </summary>
        /// <param name="isVerify"></param>
        /// <returns></returns>
        public int CountUserVerifyOrNot(bool isVerify)
        {
            using (var db = new Ws_DataContext())
            {
                int numberUser = 0;
                if (isVerify)
                {
                    numberUser = db.Ws_User.Count(x => x.IsVerify == isVerify && x.AccountType == false);
                }
                else
                {
                    numberUser = db.Ws_User.Count(x => x.IsVerify == isVerify && x.AccountType == false);
                }
                return numberUser;
            }
        }

        /// <summary>
        /// count total user of wings
        /// </summary>
        /// <param name="isVerify"></param>
        /// <returns></returns>
        public int CountTotalUser()
        {
            using (var db = new Ws_DataContext())
            {
                var numberUser = db.Ws_User.Count(x => x.AccountType == false);
                return numberUser;
            }
        }

        /// <summary>
        /// Count number of new user which have been created less than 3 day
        /// </summary>
        /// <returns></returns>
        public int CountNewUser()
        {
            using (var db = new Ws_DataContext())
            {
                DateTime dateBeforeThreeDay = DateTime.UtcNow.AddDays(-3);
                var numberUser = db.Ws_User.Count(x => x.CreatedDate >= dateBeforeThreeDay);
                return numberUser;
            }
        }

        /// <summary>
        /// Get users have been create less than 3 day
        /// </summary>
        /// <returns></returns>
        public List<Ws_User> GetNewUser()
        {
            List<Ws_User> listUser;
            using (var db = new Ws_DataContext())
            {
                DateTime dateBeforeThreeDay = DateTime.UtcNow.AddDays(-3);
                var users = db.Ws_User.OrderByDescending(x => x.CreatedDate).Where(x => x.CreatedDate >= dateBeforeThreeDay && x.AccountType == false);
                listUser = users.ToList();
            }
            return listUser;
        }
        public void Dispose()
        {
            
        }
    }
}