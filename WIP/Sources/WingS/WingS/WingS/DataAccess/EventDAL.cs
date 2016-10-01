using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
namespace WingS.DataAccess
{
    public class EventDAL : IDisposable
    {

        public List<Event> GetTopEventByView(int eventNumber)
        {
            List<Event> list = null;
        
            using (var db = new Ws_DataContext())
            {
                var topEvent = db.Events.OrderByDescending(x => x.Event_Statistic.Views).Take(eventNumber);
                list = topEvent.ToList();
            }
            return list;

        }

        /// <summary>
        /// Get all images off an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public List<EventAlbumImage> GetAllImageEventById(int eventId)
        {
            List<EventAlbumImage> eventImagesList;

            using (var db = new Ws_DataContext())
            {
                var imageList = db.EventAlbum.Where(x => x.EventId == eventId);
                eventImagesList = imageList.ToList();
            }

            return eventImagesList;
        }

        /// <summary>
        /// Get main image of an Event
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

        public void Dispose()
        {
          
        }
    }
}