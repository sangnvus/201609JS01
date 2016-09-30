using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                var topThread = db.Threads.OrderByDescending(x => x.Views).Take(threadNumber);
                listThreads = topThread.ToList();
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
            newThread.UserId = thread.CreatorID;
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
        //public List<Event> GetTopEventByView(int eventNumber)
        //{
        //    List<Event> list = null;

        //    using (var db = new Ws_DataContext())
        //    {
        //        var topEvent = db.Events.OrderByDescending(x => x.Event_Statistic.Views).Take(eventNumber);
        //        list = topEvent.ToList();
        //    }
        //    return list;

        //}
        public void Dispose()
        {
            
        }
    }
}