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
                if (userGet.Count < 1)
                {
                    string[] searchTerms = searchString.Split(' ');
                        foreach (var term in searchTerms)
                        {
                            var currentTerm = term.Trim();
                            var topThread =
                            db.Threads.OrderByDescending(x => x.CreatedDate)
                            .Where(x => x.Status == true && x.Title.Contains(currentTerm));
                            listThreads.AddRange(topThread.ToList());
                            if (listThreads.Count >= 20)
                            {
                                break;
                            }
                        }
                    int checkExisted = 0;
                    for (int i = 0; i < listThreads.Count; i++)
                    {
                        Thread currentThread = listThreads[i];
                        for (int j = 0; j < listThreads.Count; j++)
                        {
                            if (currentThread.Equals(listThreads[j])) checkExisted++;
                            if (checkExisted >= 2) listThreads.Remove(listThreads[j]);
                        }

                    }
                    return listThreads;
                }
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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                var userGet = db.User_Information.SqlQuery(SqlQuery).ToList();
                if (userGet.Count<1)
                {
                    foreach (var term in searchTerms)
                    {
                        var currentTerm = term.Trim();
                        var dataget = (from p in db.User_Information
                                       where
                                           p.FullName.Contains(currentTerm) || p.Phone.Contains(currentTerm) ||
                                           p.UserAddress.Contains((currentTerm))
                                       select p).OrderByDescending(x => x.FullName);
                        listUsers.AddRange(dataget.ToList());
                        if (listUsers.Count >= 20)
                        {
                            break;
                        }
                    }
                    int checkExisted = 0;
                    for (int i = 0; i < listUsers.Count; i++)
                    {
                        User_Information currentUser = listUsers[i];
                        for (int j = 0; j < listUsers.Count; j++)
                        {
                            if (currentUser.Equals(listUsers[j])) checkExisted++;
                            if (checkExisted >= 2) listUsers.Remove(listUsers[j]);
                        }

                    }
                    return listUsers;
                }
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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Ws_User.SqlQuery(SqlQuery).ToList();
                if (userGet.Count < 1)
                {
                    foreach (var term in searchTerms)
                    {
                        var currentTerm = term.Trim();
                        var dataget = (from p in db.Ws_User where p.UserName.Contains(currentTerm) || p.Email.Contains(currentTerm) select p);
                        listUsers.AddRange(dataget.ToList());
                        if (listUsers.Count >= 20)
                        {
                            break;
                        }
                    }
                    int checkExisted = 0;
                    for (int i = 0; i < listUsers.Count; i++)
                    {
                        Ws_User currentUser = listUsers[i];
                        for (int j = 0; j < listUsers.Count; j++)
                        {
                            if (currentUser.Equals(listUsers[j])) checkExisted++;
                            if (checkExisted >= 2) listUsers.Remove(listUsers[j]);
                        }

                    }
                    return listUsers;
                }
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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Events.SqlQuery(SqlQuery).ToList();
                if (userGet.Count<1)
                {
                    foreach (var term in searchTerms)
                    {
                        var currentTerm = term.Trim();
                        var Event =
                            db.Events.OrderByDescending(x => x.Created_Date)
                                .Where(x => x.Status == true && (x.EventName.Contains(currentTerm) || x.Location.Contains(currentTerm)));
                        listEvents.AddRange(Event.ToList());
                        if (listEvents.Count >= 20)
                        {
                            break;
                        }
                    }
                    int checkExisted = 0;
                    for (int i = 0; i < listEvents.Count; i++)
                    {
                        Event currentEvent = listEvents[i];
                        for (int j = 0; j < listEvents.Count; j++)
                        {
                            if (currentEvent.Equals(listEvents[j])) checkExisted++;
                            if (checkExisted >= 2) listEvents.Remove(listEvents[j]);
                        }
                    }
                    return listEvents;
                }
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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                var userGet = db.Organizations.SqlQuery(SqlQuery).ToList();
                if (userGet.Count < 1)
                {
                    foreach (var term in searchTerms)
                    {
                        var currentTerm = term.Trim();
                        var getOrg =
                            db.Organizations.OrderByDescending(x => x.Point)
                                .Where(
                                    x =>
                                        x.OrganizationName.Contains(currentTerm) || x.Email.Contains(currentTerm) ||
                                        x.Phone.Contains(currentTerm) || x.Address.Contains(currentTerm));
                        listOrgs.AddRange(getOrg.ToList());
                        if (listOrgs.Count >= 20)
                        {
                            break;
                        }
                    }
                    int checkExisted = 0;
                    for (int i = 0; i < listOrgs.Count; i++)
                    {
                        Organization currentoOrganization = listOrgs[i];
                        for (int j = 0; j < listOrgs.Count; j++)
                        {
                            if (currentoOrganization.Equals(listOrgs[j])) checkExisted++;
                            if (checkExisted >= 2) listOrgs.Remove(listOrgs[j]);
                        }

                    }
                    return listOrgs;
                }
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