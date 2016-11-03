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
        public List<Thread> SearchThreads(string searchString)
        {
            List<Thread> listThreads = new List<Thread>();
            string SqlQuery = "select * from Thread where FREETEXT(*, '" + searchString + "')";
            //var s = FtsInterceptor.Fts(searchString);
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
        public List<User_Information> SearchUsers(string searchString)
        {
            List<User_Information> listUsers = new List<User_Information>();
            string SqlQuery = "select * from User_Information where FREETEXT(*, '"+searchString+"')";
            //var s = FtsInterceptor.Fts(searchString);
            using (var db = new Ws_DataContext())
            {
                var userGet = db.User_Information.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listUsers.AddRange(userGet.ToList());
            }
            return listUsers;
        }
        public List<Event> SearchEvent(string searchString)
        {
            List<Event> listEvents = new List<Event>();
            string SqlQuery = "select * from Event where FREETEXT(*, '" + searchString + "')";
            //var s = FtsInterceptor.Fts(searchString);
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Events.SqlQuery(SqlQuery).ToList();
                //var userGet = db.User_Information.Where(x => x.EFullName.Contains(s)||x.Ws_User.UserName.Contains(s)||x.Ws_User.Email.Contains(s)).ToList();
                listEvents.AddRange(userGet.ToList());
            }

            return listEvents;
        }
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
        public List<Organization> SearchOrganizations(string searchString)
        {
            List<Organization> listOrgs = new List<Organization>();
            string SqlQuery = "select * from Organization where FREETEXT(*, '" + searchString + "')";
            //var s = FtsInterceptor.Fts(searchString);
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Organizations.SqlQuery(SqlQuery).ToList();
                listOrgs.AddRange(userGet.ToList());
            }
            return listOrgs;
        }
        public void Dispose()
        {
        }
    }
}