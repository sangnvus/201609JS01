﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class EventDAL : IDisposable
    {
        public int CountLikeInEvent(int EventId)
        {
            using (var db = new Ws_DataContext())
            {
                int CountLike = db.LikeEvents.Count(x => x.EventId == EventId && x.Status == true);
                return CountLike;
            }

        }
        public bool CheckUserIsLikedOrNot(int EventId)
        {
            using (var db = new Ws_DataContext())
            {
                var isLike = (from p in db.LikeEvents
                              where p.EventId == EventId && p.UserId == WsConstant.CurrentUser.UserId && p.Status == true
                              select p).SingleOrDefault();
                if (isLike != null) return true;
                else return false;
            }
        }
    
    public bool ChangelikeState(int EventId)
    {
        try
        {
            using (var db = new Ws_DataContext())
            {
                //Check current like status.
                LikeEvents current = (
                                       from p in db.LikeEvents
                                       where p.EventId == EventId && p.UserId == WsConstant.CurrentUser.UserId
                                       select p
                                       ).SingleOrDefault();
                if (current != null)
                {
                    if (current.Status == true) current.Status = false;
                    else current.Status = true;
                    db.SaveChanges();
                }
                else
                {
                    db.LikeEvents.Add(new LikeEvents() { EventId = EventId, UserId = WsConstant.CurrentUser.UserId, Status = true });
                    db.SaveChanges();
                }

                return true;

            }
        }
        catch (Exception) { return false; }
    }
        public List<int> GetAllCommentIdAndSubCommentId()
        {
            List<int> list = new List<int>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentEvent
                        .Where(x => x.Status == true)
                        .Select(x => new { x.CommentEventId });
                    foreach (var item in listComment)
                    {
                        list.Add(item.CommentEventId);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public List<BasicCommentThread> GetAllCommentInEvent(int eventId)
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.CommentEvents
                        .Where(x => x.Status == true && x.EventId == eventId)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.CommentEventId, x.Content, x.CommentDate, x.SubComment.Count })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.CommentEventId;
                        bs.Content = item.Content;
                        bs.NumberSubComment = item.Count;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public CommentEvent AddNewComment(CommentEvent comment)
        {
            using (var db = new Ws_DataContext())
            {
                var newComment = db.CommentEvents.Add(comment);
                db.SaveChanges();
                return newComment;
            }

        }
        public SubCommentEvent AddNewSubComment(SubCommentEvent subComment)
        {
            using (var db = new Ws_DataContext())
            {
                var newComment = db.SubCommentEvent.Add(subComment);
                db.SaveChanges();
                return newComment;
            }

        }
        public List<BasicCommentThread> GetSubCommentInEventById(int CommentId)
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentEvent
                        .Where(x => x.Status == true && x.CommentEventId == CommentId)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.CommentEventId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = CommentId;
                        bs.Content = item.Content;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public List<BasicCommentThread> GetAllSubCommentInEvent()
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentEvent
                        .Where(x => x.Status == true)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.CommentEventId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.CommentEventId;
                        bs.Content = item.Content;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
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
        /// Get all event sort by point
        /// </summary>
        /// <returns></returns>
        public List<Event> GetEventSortByPoint()
        {
            List<Event> listEvents = null;
            //Get All event sort by point
            using (var db = new Ws_DataContext())
            {
                var events = db.Events.OrderByDescending(x => x.TotalPoint).Where(x => x.Status == true);
                listEvents = events.ToList();
            }

            return listEvents;
        }

        public List<Event> GetEventFollowEventType(int eventType)
        {
            List<Event> listEvents = null;
            //Get All event sort by point
            using (var db = new Ws_DataContext())
            {
                var events = db.Events.OrderByDescending(x => x.TotalPoint).Where(x => x.Status == true && x.EventType == eventType);
                listEvents = events.ToList();
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
        public EventTypeDropList GetEventType()
        {
            EventTypeDropList eventType = new EventTypeDropList();
            using (var db = new Ws_DataContext())
            {
                var eventtypeList = db.EventTypes.ToList();
                foreach (var eventtype in eventtypeList)
                {
                    eventType.EventTypeList.Add(new SelectListItem
                    {
                        Text = eventtype.EventName,
                        Value = eventtype.EventTypeID.ToString()
                    });
                }
                if (eventType.EventTypeList.Count > 0)
                {
                    eventType.Selected = 1;
                }
                return eventType;
            }
        }
        public List<EventTimeLine> GetEventTimeLineByEventId(int eventId)
        {
            using (var db = new Ws_DataContext())
            {
                var currentTimeLine = db.ETimeLine.OrderByDescending(x => x.EventId == eventId && x.Status == true).ToList();
                return currentTimeLine;
            }
        }
        public EventBasicInfo GetEventBasicInfoById(int eventId)
        {
            EventBasicInfo EvtBasicInfo = new EventBasicInfo();
            using (var db = new Ws_DataContext())
            {
                var currentEvent = db.Events.FirstOrDefault(x => x.EventID == eventId);
                EvtBasicInfo.CreatorID = currentEvent.CreatorID;
                EvtBasicInfo.CreatorName = currentEvent.Organization.OrganizationName;
                EvtBasicInfo.VideoUrl = currentEvent.VideoUrl;
                EvtBasicInfo.EventName = currentEvent.EventName;
                EvtBasicInfo.EventType = currentEvent.EType.EventName;
                EvtBasicInfo.CreatedDate = currentEvent.Created_Date.ToString("dd/MM/yyyy");
                EvtBasicInfo.Content = currentEvent.Description;
                EvtBasicInfo.ExpectedMoney = currentEvent.ExpectedMoney;
                EvtBasicInfo.Location = currentEvent.Location;
                EvtBasicInfo.ContactInfo = currentEvent.Contact;
                EvtBasicInfo.ShortDescription = currentEvent.ShortDescription;
                EvtBasicInfo.Start_Date = currentEvent.Start_Date.ToString("dd/MM/yyyy"); ;
                EvtBasicInfo.Finish_Date = currentEvent.Finish_Date.ToString("dd/MM/yyyy");
                EvtBasicInfo.CreatorUserName = currentEvent.Organization.Ws_User.UserName;
            }
            //Get ImageAlbum
            using (var db = new Ws_DataContext())
            {
                var imgInEvent = (from p in db.EventAlbum
                                  where p.EventId == eventId
                                  select p.ImageUrl).ToList();
                EvtBasicInfo.ImageAlbum = imgInEvent;
            }
            return EvtBasicInfo;
        }
        public Organization GetOrganizationById(int orgId)
        {
            using (var db = new Ws_DataContext())
            {
                var organi = db.Organizations.FirstOrDefault(x => x.OrganizationId == orgId);
                return organi;
            }

        }
        public Event AddNewEvent(CreateEventInfo eventInfo)
        {
            var newEvent = CreateEmptyEvent();
            newEvent.CreatorID = GetOrganizationById(WsConstant.CurrentUser.UserId).OrganizationId;
            newEvent.EventType = eventInfo.EventType;
            newEvent.EventName = eventInfo.EventName;
            newEvent.EEventName = ConvertToUnSign.Convert(newEvent.EventName);
            if (eventInfo.StartDate != "")
            {
                newEvent.Start_Date = DateTime.Parse(eventInfo.StartDate);
            }
            if (eventInfo.FinishDate != "")
            {
                newEvent.Finish_Date = DateTime.Parse(eventInfo.FinishDate);
            }
            newEvent.ShortDescription = eventInfo.ShortDescription;
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
            if (eventInfo.FromDate != "")
            {
                newEventSchedule.FromDate = DateTime.Parse(eventInfo.FromDate);
            }
            if (eventInfo.ToDate != "")
            {
                newEventSchedule.ToDate = DateTime.Parse(eventInfo.ToDate);
            }
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

        /// <summary>
        /// Get all event of a organization with its id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns>List of Event</returns>
        public List<Event> GetEventsBelongToCreator(int organizationId)
        {
            List<Event> eventsOfCreator;

            using (var db = new Ws_DataContext())
            {
                var listEvent = db.Events.OrderByDescending(x => x.Created_Date).Where(x => x.Status == true && x.CreatorID == organizationId);
                eventsOfCreator = listEvent.ToList();
            }

            return eventsOfCreator;
        }

        /// <summary>
        /// Get Event that has greatest point of a Organization
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>Event</returns>
        public List<Event> GetTopOneEventOfOrganization(int orgId)
        {
            List<Event> list;

            using (var db = new Ws_DataContext())
            {
                var topEvent = db.Events.OrderByDescending(x => x.TotalPoint).Where(x => x.Status == true && x.CreatorID == orgId).Take(1);
                list = topEvent.ToList();
            }
            return list;

        }

        public void Dispose()
        {
          
        }
    }
}