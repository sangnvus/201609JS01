using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
namespace WingS.DataAccess
{
    public class EventDAL : IDisposable
    {
        public Event GetTopViewEvent()
        {
            using (var db = new Ws_DataContext())
            {
                var topEvent = db.Events.FirstOrDefault(x=>x.EventID==2);
                return topEvent;
            }
        }
        public void Dispose()
        {
          
        }
    }
}