using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class ThreadDAL : IDisposable
    {
        public List<Thread> GetTopThreadByView(int threadNumber)
        {
            List<Thread> listThreads = null;

            using (var db = new Ws_DataContext())
            {
                var topThread = db.Threads.OrderByDescending(x => x.Views).Where(x=>x.Status== true).Take(threadNumber);
                listThreads = topThread.ToList();
            }

            return listThreads;
        }
        // Get Thread by Created
        public List<Thread> GetNewestThreadByCreatedDate()
        {
            List<Thread> listThreads = null;
            //Get All thread by created Date
            using (var db = new Ws_DataContext())
            {
                var newestThread = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true);
                listThreads = newestThread.ToList();
            }

            return listThreads;
        }
        public Thread GetThreadById(int threadId)
        {
            using (var db = new Ws_DataContext())
            {
                var currentThread = db.Threads.FirstOrDefault(x => x.ThreadId == threadId);
                return currentThread;
            }
          
        }
        public Thread AddNewThread(CreateThreadInfo thread)
        {
            var newThread = CreateEmptyThread();
            newThread.UserId = WsConstant.CurrentUser.UserId;
            newThread.Title = thread.Title;
            newThread.Content = thread.Content;
            using (var db = new Ws_DataContext())
                 {
                     db.Threads.Add(newThread);
                     db.SaveChanges();
                     return GetThreadById(newThread.ThreadId);
                 }

        }
        public Thread CreateEmptyThread()
        {
            using (var db = new Ws_DataContext())
            {
                var thread = db.Threads.Create();
                thread.UserId = 0;
                thread.Title = "";
                thread.Content = "";
                thread.VideoUrl = "";
                thread.CreatedDate = DateTime.Now;
                thread.UpdatedDate = DateTime.Now;
                thread.Views = 0;
                thread.Likes = 0;
                thread.Status = true;
                return thread;
            }
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
        /// Get main image of an Thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public ThreadAlbumImage GetMainImageThreadById(int threadId)
        {
            ThreadAlbumImage threadMainImage;

            using (var db = new Ws_DataContext())
            {
                var image = db.ThreadAlbum.FirstOrDefault(x => x.ThreadId == threadId && x.status == true);
                threadMainImage = image;
            }

            return threadMainImage;
        }

        public void Dispose()
        {
            
        }
    }
}