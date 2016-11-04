using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class SearchDAL : IDisposable
    {
        /// <summary>
        /// Search Thread
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Thread> SearchThreads(string searchString)
        {
            List<Thread> listThreads = new List<Thread>();
            string SqlQuery = "select * from Thread where FREETEXT(*, '" + searchString + "')";
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Threads.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listThreads.AddRange(userGet.ToList());
            }

            return listThreads;
        }
        /// <summary>
        /// Get all image of an Thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public List<String> GetAllImageThreadById(int threadId)
        {
            List<ThreadAlbumImage> threadImagesList;
            List<String> threadImagesUrlList = new List<string>();

            using (var db = new Ws_DataContext())
            {
                var imageList = db.ThreadAlbum.Where(x => x.ThreadId == threadId);
                threadImagesList = imageList.ToList();
            }

            foreach (ThreadAlbumImage threadImage in threadImagesList)
            {
                String url = threadImage.ImageUrl;
                threadImagesUrlList.Add(url);
            }

            return threadImagesUrlList;
        }
        /// <summary>
        /// Search User information
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<User_Information> SearchUserInfo(string searchString)
        {
            List<User_Information> listUsers = new List<User_Information>();
            string SqlQuery = "select * from User_Information where FREETEXT(*, '"+searchString+"')";
            using (var db = new Ws_DataContext())
            {
                var userGet = db.User_Information.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listUsers.AddRange(userGet.ToList());
            }
            return listUsers;
        }
        /// <summary>
        /// Seach in WS_User
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Ws_User> SearchUsers(string searchString)
        {
            List<Ws_User> listUsers = new List<Ws_User>();
            string SqlQuery = "select * from Ws_User where FREETEXT(*, '" + searchString + "')";
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Ws_User.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listUsers.AddRange(userGet.ToList());
            }
            return listUsers;
        }
        /// <summary>
        /// Seach Event
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Event> SearchEvent(string searchString)
        {
            List<Event> listEvents = new List<Event>();
            string SqlQuery = "select * from Event where FREETEXT(*, '" + searchString + "')";
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Events.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listEvents.AddRange(userGet.ToList());
            }

            return listEvents;
        }
        /// <summary>
        /// get main event by event id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public EventAlbumImage GetMainImageEventById(int eventId)
        {
            EventAlbumImage eventMainImage;

            using (var db = new Ws_DataContext())
            {
                var image = db.EventAlbum.FirstOrDefault(x => x.EventId == eventId && x.status == true);
                eventMainImage = image;
            }

            return eventMainImage;
        }
        /// <summary>
        /// Search organization
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Organization> SearchOrganizations(string searchString)
        {
            List<Organization> listOrgs = new List<Organization>();
            string SqlQuery = "select * from Organization where FREETEXT(*, '" + searchString + "')";
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Organizations.SqlQuery(SqlQuery).ToList();
                listOrgs.AddRange(userGet.ToList());
            }
            return listOrgs;
        }

        public string GetUserNameById(int userId)
        {
            string userNm="";
            using (var db = new Ws_DataContext())
            {
                var value = db.Ws_User.FirstOrDefault(x => x.UserID == userId);
                if (value != null)
                {
                    userNm = value.UserName;
                }
            }
            return userNm;
        }
        public SearchUser GetUserInfoById(int userId,string userNm)
        {
            SearchUser userinfo = null;
            using (var db = new Ws_DataContext())
            {
                var value = db.User_Information.FirstOrDefault(x => x.UserID == userId);
                if (value != null)
                {
                    userinfo = new SearchUser(value.UserID,userNm,value.FullName,value.ProfileImage,value.UserAddress,value.Phone,value.FacebookUrl);
                }
            }
            return userinfo;
        }
        public void Dispose()
        {
        }
    }
}