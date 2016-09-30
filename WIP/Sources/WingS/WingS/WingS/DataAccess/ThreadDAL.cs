using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;

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