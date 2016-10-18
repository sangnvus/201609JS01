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

        public Event GetEventById(int eventId)
        {
            using (var db = new Ws_DataContext())
            {
                var currentThread = db.Events.FirstOrDefault(x => x.EventID == eventId);
                return currentThread;
            }

        }
        public Organization GetOrganiIdByUserId(int userId)
        {
            using (var db = new Ws_DataContext())
            {
                var organi = db.Organizations.FirstOrDefault(x => x.UserId == userId);
                return organi;
            }

        }
        public Event AddNewEvent(CreateEventInfo eventInfo)
        {
            var newEvent = CreateEmptyEvent();
            newEvent.CreatorID = GetOrganiIdByUserId(WsConstant.CurrentUser.UserId).OrganizationId;
            newEvent.EventType = eventInfo.EventType;
            newEvent.EventName = eventInfo.EventName;
            newEvent.Start_Date = DateTime.Parse(eventInfo.StartDate);
            newEvent.Finish_Date = DateTime.Parse(eventInfo.FinishDate);
            newEvent.Location = eventInfo.Location;
            newEvent.ExpectedMoney = eventInfo.ExpectedMoney;
            newEvent.Description = eventInfo.Content;
            newEvent.Contact = eventInfo.Contact;
            newEvent.VideoUrl = eventInfo.VideoUrl;
            using (var db = new Ws_DataContext())
            {
                db.Events.Add(newEvent);
                db.SaveChanges();
                return GetEventById(newEvent.EventID);
            }

        }
        public void AddNewEventTimeLine(CreateEventSchedule eventInfo, int eventId)
        {
            var newEventSchedule = CreateEmptyEventTimeLine();
            newEventSchedule.EventId = eventId;
            newEventSchedule.Content = eventInfo.Description;
            //newEventSchedule.FromDate =  DateTime.ParseExact(eventInfo.FromDate, "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture);
            //newEventSchedule.ToDate = DateTime.ParseExact(eventInfo.ToDate, "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture);
            newEventSchedule.FromDate = DateTime.Parse(eventInfo.FromDate);
            newEventSchedule.ToDate = DateTime.Parse(eventInfo.ToDate);
            using (var db = new Ws_DataContext())
            {
                db.ETimeLine.Add(newEventSchedule);
                db.SaveChanges();
            }

        }
        public Event CreateEmptyEvent()
        {
            using (var db = new Ws_DataContext())
            {
                var eventInfo = db.Events.Create();
                eventInfo.CreatorID = 0;
                eventInfo.EventType = 0;
                eventInfo.EventName = "";
                eventInfo.Location = "";
                eventInfo.Contact = "";
                eventInfo.Description = "";
                eventInfo.TotalPoint = 0;
                eventInfo.VideoUrl = "";
                eventInfo.Created_Date = DateTime.Now;
                eventInfo.Updated_Date = DateTime.Now;
                eventInfo.Status = true;
                return eventInfo;
            }
        }

        public EventTimeLine CreateEmptyEventTimeLine()
        {
            using (var db = new Ws_DataContext())
            {
                var eventInfo = db.ETimeLine.Create();
                eventInfo.EventId = 0;
                eventInfo.Content = "";
                eventInfo.Status = true;
                return eventInfo;
            }
        }
        public void Dispose()
        {
          
        }
    }
}