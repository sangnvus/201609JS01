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
            List<Event> top10list = GetTop3EventByView();
            Event evt = top10list.First();
            return evt;
        }
        public List<Event> GetTop3EventByView()
        {
            List<Event> list = null;
        
            using (var db = new Ws_DataContext())
            {
                    var topEvent = db.Events.OrderByDescending(x => x.Event_Statistic.Views).Take(9);
                    list = topEvent.ToList();
            }
            return list;

        }
        public void Dispose()
        {
          
        }
    }
}