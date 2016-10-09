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
            List<Thread> listThreads = null;

            using (var db = new Ws_DataContext())
            {
                var topThread = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.Title.Contains(searchString));
                listThreads = topThread.ToList();
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
            List<User_Information> listUsers = null;

            using (var db = new Ws_DataContext())
            {
                var userGet = db.User_Information.OrderByDescending(x => x.FullName).Where(x => x.FullName.Contains(searchString) || x.Phone.Contains(searchString) || x.UserAddress.Contains(searchString));
                listUsers = userGet.ToList();
            }
            return listUsers;
        }
        public List<Event> SearchEvent(string searchString)
        {
            List<Event> listEvents = null;
            //Get All event by created Date
            using (var db = new Ws_DataContext())
            {
                var Event = db.Events.OrderByDescending(x => x.Created_Date).Where(x => x.Status == true && x.EventName.Contains(searchString));
                listEvents = Event.ToList();
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
        public List<Organazation> SearchOrganazations(string searchString)
        {
            List<Organazation> orgList = null;
            using (var db = new Ws_DataContext())
            {
                var getOrg = db.Organazations.OrderByDescending(x => x.Point).Where(x => x.OrganazationName == searchString || x.Email == searchString || x.Phone == searchString || x.Address == searchString);
                orgList = getOrg.ToList();
            }
            return orgList;
        }
        public void Dispose()
        {
        }
    }
}