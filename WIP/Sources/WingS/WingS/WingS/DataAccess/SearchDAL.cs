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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                foreach (var term in searchTerms)
                {
                    var currentTerm = term.Trim();
                    var topThread =
                    db.Threads.OrderByDescending(x => x.CreatedDate)
                    .Where(x => x.Status == true && x.Title.Contains(currentTerm));
                    listThreads.AddRange(topThread.ToList());
                    if (listThreads.Count >= 30)
                    {
                        break;
                    }
                }
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
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
            {
                foreach (var term in searchTerms)
                {
                    var currentTerm = term.Trim();
                    var userGet = (from p in db.User_Information
                        where
                            p.FullName.Contains(currentTerm) || p.Phone.Contains(currentTerm) ||
                            p.UserAddress.Contains((currentTerm))
                        select p).OrderByDescending(x => x.FullName).Distinct();
                    listUsers.AddRange(userGet.ToList());
                    if (listUsers.Count >= 30)
                    {
                        break;
                    }
                }
            }
            
            int checkExisted = 0;
            for (int i = 0; i < listUsers.Count; i++)
            {
                User_Information  currentUser = listUsers[i];
                for (int j = 0; j < listUsers.Count; j++)
                {
                        if (currentUser.Equals(listUsers[j])) checkExisted++;
                        if (checkExisted >= 2) listUsers.Remove(listUsers[j]);
                }
             
            }
            return listUsers;
        }
        public List<Event> SearchEvent(string searchString)
        {
            List<Event> listEvents = new List<Event>();
            string[] searchTerms = searchString.Split(' ');
            //Get All event by created Date
            using (var db = new Ws_DataContext())
            {
                foreach (var term in searchTerms)
                {
                    var currentTerm = term.Trim();
                    var Event =
                        db.Events.OrderByDescending(x => x.Created_Date)
                            .Where(x => x.Status == true && x.EventName.Contains(currentTerm));
                    listEvents.AddRange(Event.ToList());
                    if (listEvents.Count >= 30)
                    {
                        break;
                    }
                }
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
            List<Organization> orgList = new List<Organization>();
            string[] searchTerms = searchString.Split(' ');
            using (var db = new Ws_DataContext())
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
                    orgList.AddRange(getOrg.ToList());
                    if (orgList.Count >= 30)
                    {
                        break;
                    }
                }
            }
            return orgList;
        }
        /// <summary>
        /// Chuyển tiếng việt có dấu thành tiếng việt không dấu (khoảng trắng thay  bằng dấu -)
        /// </summary>
        /// <param name="strVietnamese">tiếng việt có dấu</param>
        /// <returns>tiếng việt không dấu</returns>
        public string ConvertToVietnameseNotSignature(string strVietnamese)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ #%&*:|.";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY-       ";
            int index = -1;
            while ((index = strVietnamese.IndexOfAny(FindText.ToCharArray())) != -1)
            {
                int index2 = FindText.IndexOf(strVietnamese[index]);
                strVietnamese = strVietnamese.Replace(strVietnamese[index], ReplText[index2]);
            }
            return strVietnamese;
        }
        public void Dispose()
        {
        }
    }
}