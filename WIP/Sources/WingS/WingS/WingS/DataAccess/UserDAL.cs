using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
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
        public UserBasicInfoDTO GetUserInfoUsingUserNameOrEmail(string userNameOrEmail)
        {
            UserBasicInfoDTO userBasic = new UserBasicInfoDTO();
            try
            {
                Ws_User user;
                using (var db = new Ws_DataContext())
                {
                    user = db.Ws_User.FirstOrDefault(x => x.UserName == userNameOrEmail || x.Email == userNameOrEmail);
                }

                userBasic = GetFullInforOfUserAsBasicUser(user.UserID);
            }
            catch (Exception)
            {
                //throw;
            }

            return userBasic;
        }

        /// <summary>
        /// Get full information of User
        /// #Note: If you add new field to UserBasicInfoDTO model -
        ///        Please write code to set that information in this function
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserBasicInfoDTO</returns>
        public UserBasicInfoDTO GetFullInforOfUserAsBasicUser(int userId)
        {
            UserBasicInfoDTO currentUser = new UserBasicInfoDTO();

            int numberEventDonatedIn = 0;
            decimal totalMoneyDonatedIn = 0;
            decimal lastDonateMoney = 0;
            string lastDonateDate = "";
            
            int numberOfPost = 0;

            try
            {
                Ws_User wsUser = GetUserById(userId);
                User_Information userInformation = GetUserInformation(userId);

                //Get infomation about donation of this user
                using (var db = new DonationDAL())
                {
                    numberEventDonatedIn = db.GetNumberEventDonatedInByUsingUserId(userId);
                    totalMoneyDonatedIn = db.GetTotalMoneyDonatedInByUsingUserId(userId);

                    Donation lastDonation = db.GetLastDonateInformation(userId);

                    if (lastDonation != null)
                    {
                        lastDonateMoney = lastDonation.DonatedMoney;
                        lastDonateDate = lastDonation.DonatedDate.ToString("H:mm:ss dd/MM/yy");
                    }
                }

                //Get number of post for current user
                using (var db = new ThreadDAL())
                {
                    numberOfPost = db.GetNumberOfPostPerUser(userId);
                }

                //get ranking information
                WsRanking ranking = new WsRanking();
                RankingDTO rank = ranking.RankingWithPoint(userInformation.Point);
               

                //Set information for user which want to get
                currentUser.UserId = userId;
                currentUser.UserName = wsUser.UserName;
                currentUser.AccountType = wsUser.AccountType;
                currentUser.IsActive = wsUser.IsActive;
                currentUser.IsVerify = wsUser.IsVerify;
                currentUser.FullName = userInformation.FullName;
                currentUser.ProfileImage = userInformation.ProfileImage;
                currentUser.Email = wsUser.Email;
                currentUser.Gender = userInformation.Gender;
                currentUser.Phone = userInformation.Phone;
                currentUser.Address = userInformation.UserAddress;
                currentUser.NumberOfPost = numberOfPost;
                if (userInformation.DoB != null)
                {
                    currentUser.DOB = userInformation.DoB.Value.ToString("dd/MM/yyyy");
                }

                currentUser.Country = userInformation.Country;
                currentUser.FacebookUri = userInformation.FacebookUrl;
                currentUser.CreateDate = wsUser.CreatedDate.ToString("H:mm:ss dd/MM/yy");
                currentUser.Point = userInformation.Point;
                if (rank.CurrentRank==0)
                {
                    currentUser.CurrentRank = "New";
                }
                else if (rank.CurrentRank == 200)
                {
                    currentUser.CurrentRank = "Bronze";
                }
                else if (rank.CurrentRank == 500)
                {
                    currentUser.CurrentRank = "Silver";
                }
                else if (rank.CurrentRank == 2000)
                {
                    currentUser.CurrentRank = "Golden";
                }
                else if (rank.CurrentRank == 5000)
                {
                    currentUser.CurrentRank = "Plantium";
                }
                else if (rank.CurrentRank == 10000)
                {
                    currentUser.CurrentRank = "Diamon";
                }

                currentUser.RankPercent = rank.RankPercent;
                currentUser.NumberEventDonatedIn = numberEventDonatedIn;
                currentUser.TotalMoneyDonatedIn = totalMoneyDonatedIn;
                currentUser.LastDonateMoney = lastDonateMoney;
                currentUser.LastDonateDate = lastDonateDate;
            }
            catch (Exception)
            {
                
                //throw;
            }

            return currentUser;
        }

        /// <summary>
        /// Get user using user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ws_User GetUserById(int id)
        {
            Ws_User currentUser = new Ws_User();
            try
            {
                using (var db = new Ws_DataContext())
                {
                    currentUser = db.Ws_User.FirstOrDefault(x => x.UserID == id);
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            return currentUser;
        }

        /// <summary>
        /// get user infomation using user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User_Information GetUserInformation(int userId)
        {
            User_Information currentUserInfo = new User_Information();
            try
            {
                using (var db = new Ws_DataContext())
                {
                    currentUserInfo = db.User_Information.FirstOrDefault(x => x.UserID == userId);
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            return currentUserInfo;
        }
        /// <summary>
        /// Get All User in WS_USER
        /// </summary>
        /// <returns>list ws_user</returns>
        public List<UserBasicInfoDTO> GetAllUser()
        {
            var listUser = new List<UserBasicInfoDTO>();
            try
            {
                List<int> userIdList;
                using (var db = new Ws_DataContext())
                {
                    userIdList = db.Ws_User.Select(x => x.UserID).ToList();
                }

                using (var db = new UserDAL())
                {
                    foreach (int userId in userIdList)
                    {
                        UserBasicInfoDTO userBasic = db.GetFullInforOfUserAsBasicUser(userId);
                        listUser.Add(userBasic);
                    }
                }

            }
            catch (Exception)
            {
                //throw;
            }

            return listUser;
        }
        /// <summary>
        /// Get User Name By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetFullNameById(int userId)
        {
            string userNm = "";
            using (var db = new Ws_DataContext())
            {
                var value = db.User_Information.FirstOrDefault(x => x.UserID == userId);
                if (value != null)
                {
                    userNm = value.FullName;
                }
            }
            return userNm;
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
        /// Count total event in database
        /// </summary>
        /// <returns>int</returns>
        public int CountTotalEvent()
        {
            int numberUser = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberUser = db.Events.Count();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberUser;
        }

        /// <summary>
        /// Count total Thread in database
        /// </summary>
        /// <returns>int</returns>
        public int CountTotalThread()
        {
            int numberUser = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberUser = db.Threads.Count();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberUser;
        }

        /// <summary>
        /// Count total Organization in database
        /// </summary>
        /// <returns>int</returns>
        public int CountTotalOrganization()
        {
            int numberUser = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberUser = db.Organizations.Count();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberUser;
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

        [HttpGet]
        public List<UserBasicInfoDTO> GetTopNumberRankingUser(int top)
        {
            var topRankingUser = new List<UserBasicInfoDTO>();

            try
            {
                List<int> listUserId;
                using (var db = new Ws_DataContext())
                {
                    listUserId = db.User_Information.OrderByDescending(x=>x.Point).Select(x=>x.UserID).Take(5).ToList();
                }

                foreach (int userId in listUserId)
                {
                    UserBasicInfoDTO userBasic = GetFullInforOfUserAsBasicUser(userId);
                    topRankingUser.Add(userBasic);
                }

            }
            catch (Exception)
            {

                //throw;
            }
            return topRankingUser;
        }

        /// <summary>
        /// Get top user who create most thread
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<UserBasicInfoDTO> GetTopNumberThreadCreator(int top)
        {
            List<UserBasicInfoDTO> topThreadCreator = new List<UserBasicInfoDTO>();

            try
            {
                List<UserBasicInfoDTO> listUser = new List<UserBasicInfoDTO>();


                // lay ra nhung userid co trong bang Thread
                List<int> listUserIdInDonation;

                using (var db = new Ws_DataContext())
                {
                    var listUserId = db.Threads.Select(x => x.UserId).Distinct();
                    listUserIdInDonation = listUserId.ToList();
                }

                // Lay thong tin cua nhung user ma co id trong Donation
                foreach (int userId in listUserIdInDonation)
                {
                    UserBasicInfoDTO userBasic;

                    using (var db = new UserDAL())
                    {
                        userBasic = db.GetFullInforOfUserAsBasicUser(userId);
                    }

                    listUser.Add(userBasic);
                }

                // Lay top 10 user donate nhieu nhat
                topThreadCreator = listUser.OrderByDescending(x => x.NumberOfPost).Take(top).ToList();


            }
            catch (Exception)
            {

                //throw;
            }
            return topThreadCreator;
        } 
         
        public void Dispose()
        {
            
        }
    }
}