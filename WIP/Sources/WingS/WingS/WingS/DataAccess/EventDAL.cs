using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class EventDAL : IDisposable
    {

        public List<Event> GetTopFourEventByPoint(int eventNumber)
        {
            List<Event> list = null;
        
            using (var db = new Ws_DataContext())
            {
                var topEvent = db.Events.OrderByDescending(x => x.TotalPoint).Where(x => x.Status == true).Take(eventNumber);
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
        // Get Event by Created
        public List<Event> GetNewestEventByCreatedDate()
        {
            List<Event> listEvents = null;
            //Get All event by created Date
            using (var db = new Ws_DataContext())
            {
                var newestEvent = db.Events.OrderByDescending(x => x.Created_Date).Where(x => x.Status == true);
                listEvents = newestEvent.ToList();
            }

            return listEvents;
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
        /*public Event AddNewThread(CreateEventInfo eventInfo)
        {
            var newEvent = CreateEmptyEvent();
            newThread.UserId = WsConstant.CurrentUser.UserId;
            newThread. = eventInfo.EventType;
            newThread.Content = eventInfo.Content;
            using (var db = new Ws_DataContext())
            {
                db.Threads.Add(newThread);
                db.SaveChanges();
                return GetThreadById(newThread.ThreadId);
            }

        }*/
        public Event CreateEmptyEvent()
        {
            using (var db = new Ws_DataContext())
            {
                var eventInfo = db.Events.Create();
                eventInfo.CreatorID = 0;
                eventInfo.EventType = 0;
                eventInfo.EventName = "";
                eventInfo.Location = "";
                eventInfo.Description = "";
                eventInfo.TotalPoint = 0;
                eventInfo.VideoUrl = "";
                eventInfo.Created_Date = DateTime.Now;
                eventInfo.Updated_Date = DateTime.Now;
                eventInfo.Status = true;
                return eventInfo;
            }
        }
        public void Dispose()
        {
          
        }
    }
}