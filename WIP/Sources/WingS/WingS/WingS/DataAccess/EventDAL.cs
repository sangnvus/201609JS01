using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
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
        public int CountLikeInCommentEvent(int CommentId)
        {
            using (var db = new Ws_DataContext())
            {
                int CountLike = db.LikeCommentEvents.Count(x => x.CommentId == CommentId && x.Status == true);
                return CountLike;
            }

        }
        public int CountCommentInEvent(int EventId)
        {
            using (var db = new Ws_DataContext())
            {
                int CountComment = db.CommentEvents.Count(x => x.EventId == EventId && x.Status == true);
                return CountComment;
            }

        }
        public bool CheckUserIsLikedOrNot(int EventId, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            using (var db = new Ws_DataContext())
            {
                var isLike = (from p in db.LikeEvents
                              where p.EventId == EventId && p.UserId == CurrenUser && p.Status == true
                              select p).SingleOrDefault();
                if (isLike != null) return true;
                else return false;
            }
        }
    
    public bool ChangelikeState(int EventId, string UserName)
    {
        try
        {
            int CurrenUser = 0;
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
                }
                using (var db = new Ws_DataContext())
            {
                //Check current like status.
                LikeEvents current = (
                                       from p in db.LikeEvents
                                       where p.EventId == EventId && p.UserId == CurrenUser
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
                    db.LikeEvents.Add(new LikeEvents() { EventId = EventId, UserId = CurrenUser, Status = true });
                    db.SaveChanges();
                }
                return true;
            }
        }
        catch (Exception) { return false; }
    }
        public bool ChangelikeStateForComment(int commentId, string UserName)
        {
            try
            {
                int CurrenUser = 0;
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
                }
                using (var db = new Ws_DataContext())
                {
                    //Check current like status.
                    LikeCommentEvent current = (
                                           from p in db.LikeCommentEvents
                                           where p.CommentId == commentId && p.UserId == CurrenUser
                                           select p
                                           ).SingleOrDefault();
                    if (current != null)
                    {
                        current.Status = !current.Status;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.LikeCommentEvents.Add(new LikeCommentEvent() { CommentId = commentId, UserId = CurrenUser, Status = true });
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
        public List<MessageBasicInfoDTO> GetAllPublicMessage(int eventId)
        {
            List<MessageBasicInfoDTO> list = new List<MessageBasicInfoDTO>();
            using (var db = new Ws_DataContext())
            {
                var current = (from p in db.PublicMessageDetails
                               where p.EventId == eventId
                               orderby p.CreatedDate
                               select new { p, p.User.User_Information.ProfileImage, p.User.UserName }).ToList();
                              
                            
                foreach(var item in current)
                {
                    MessageBasicInfoDTO mess = new MessageBasicInfoDTO();
                    mess.Content = item.p.Message;
                    mess.CreatedDate = item.p.CreatedDate.ToString("H:mm dd/MM");
                    mess.CreatorImage = item.ProfileImage;
                    mess.CreatorName = item.UserName;
                    list.Add(mess);
                }
            }
            return list;
        }
        public List<BasicCommentThread> GetAllCommentInEvent(int eventId,string currentUserName)
        {
            int CurrenUser = 0;
            if (currentUserName != "")
            {
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(currentUserName).UserID;
                }
            }
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
                        if(currentUserName!="")
                        { if (db.LikeCommentEvents.Where(x => x.CommentId == item.CommentEventId && x.UserId == CurrenUser && x.Status == true).SingleOrDefault() != null)
                          {
                            bs.isLiked = true;
                          }
                        }
                        else bs.isLiked = false;
                        if (CurrenUser == item.UserId)
                        {
                            bs.isDeleted = true;
                        }
                        else bs.isDeleted = false; 
                        bs.Content = item.Content;
                        bs.NumberSubComment = item.Count;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        bs.NumberOfLikes = db.LikeCommentEvents.Where(x => x.CommentId == item.CommentEventId && x.Status == true).Count();
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
        public List<BasicCommentThread> GetSubCommentInEventById(int CommentId, string currentUserName)
        {
            int CurrenUser = 0;
            if (currentUserName != "")
            {
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(currentUserName).UserID;
                }
            }
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentEvent
                        .Where(x => x.Status == true && x.CommentEventId == CommentId)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.SubCommentEventId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.SubCommentEventId;
                        if (CurrenUser == item.UserId)
                        {
                            bs.isDeleted = true;
                        }
                        else bs.isDeleted = false;
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
                var currentEvent = db.Events.FirstOrDefault(x => x.EventID == eventId);
                return currentEvent;
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
                        Text = eventtype.EventTypeName,
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
                EvtBasicInfo.EventType = currentEvent.EType.EventTypeName;
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
        public Event AddNewEvent(CreateEventInfo eventInfo, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            var newEvent = CreateEmptyEvent();
            newEvent.CreatorID = GetOrganizationById(CurrenUser).OrganizationId;
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
        public List<EventBasicInfo> GetEventsBelongToCreator(int organizationId)
        {
            List<int> eventIdList;
            using (var db = new Ws_DataContext())
            {
                eventIdList = db.Events.OrderByDescending(x => x.Created_Date).Where(x => x.Status == true && x.CreatorID == organizationId).Select(x=>x.EventID).ToList();
            }

            return eventIdList.Select(GetFullEventBasicInformation).ToList();
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

        public EventCircleTileDTO GetEventCircleTile()
        {
            EventCircleTileDTO circleInfor = new EventCircleTileDTO();
            using (var db = new Ws_DataContext())
            {
                circleInfor.NumberInComeEvent = db.Events.Count(x => x.Status && DateTime.Now < x.Start_Date);
                circleInfor.NumberActiveEvent = db.Events.Count(x => x.Status && DateTime.Now > x.Start_Date && DateTime.Now < x.Finish_Date);
                circleInfor.NumberDoneEvent = db.Events.Count(x => x.Status && DateTime.Now > x.Finish_Date);
                circleInfor.NumberBanEvent = db.Events.Count(x => !x.Status);
                circleInfor.NumberAllEvent = db.Events.Count();
                
            }
            return circleInfor;
        }

        public EventBasicInfo GetFullEventBasicInformation(int eventId)
        {
            EventBasicInfo eventBasicInfo = new EventBasicInfo();

            try
            {
                Event wsEvent;
                string creatorUserName="";
                string creatorName="";
                string organizationName = "";
                List<string> imageAlbum;
                decimal raisedMoney;

                using (var db = new Ws_DataContext())
                {
                    //Get event model
                    wsEvent = db.Events.FirstOrDefault(x => x.EventID == eventId);
                    //Get user name
                    var userGet1 = db.Ws_User.FirstOrDefault(x => x.UserID == wsEvent.CreatorID);
                    if (userGet1!=null)
                    {
                        creatorUserName = userGet1.UserName;
                    }
                    //Get user full name
                    var userGet2 = db.User_Information.FirstOrDefault(x => x.UserID == wsEvent.CreatorID);
                    if (userGet2 != null)
                    {
                        creatorName = userGet2.FullName;
                    }
                    //Get image album
                    imageAlbum = db.EventAlbum.Where(x => x.EventId == eventId).Select(x => x.ImageUrl).ToList();
                    var organiGet = db.Organizations.FirstOrDefault(x => x.OrganizationId == wsEvent.CreatorID);
                    if (organiGet != null)
                    {
                        organizationName = organiGet.OrganizationName;
                    }

                    //Get total money which has been raised
                    raisedMoney = GetTotalRaisedMoneyOfEvent(eventId);
                
                // Get main image
                string mainImageUrl = GetMainImageEventById(eventId).ImageUrl;
                //Get image album
                

                eventBasicInfo.EventID = eventId;
                eventBasicInfo.CreatorID = wsEvent.CreatorID;
                eventBasicInfo.CreatorUserName = creatorUserName;
                eventBasicInfo.OrganizationName = organizationName;
                eventBasicInfo.CreatorName = creatorName;
                eventBasicInfo.OrganizationName = wsEvent.Organization.OrganizationName;
                eventBasicInfo.CreatorName = creatorName;
                eventBasicInfo.EventName = wsEvent.EventName;
                eventBasicInfo.ShortDescription = wsEvent.ShortDescription;
                eventBasicInfo.Content = wsEvent.Description;
                eventBasicInfo.ContactInfo = wsEvent.Contact;
                eventBasicInfo.MainImageUrl = mainImageUrl;
                eventBasicInfo.ImageAlbum = imageAlbum;
                eventBasicInfo.Location = wsEvent.Location;
                eventBasicInfo.VideoUrl = wsEvent.VideoUrl;
                eventBasicInfo.Status = wsEvent.Status;
                if (!wsEvent.Status)
                {
                    eventBasicInfo.TimeStatus = "ban";
                }
                else if (wsEvent.Status && DateTime.Now > wsEvent.Finish_Date)
                {
                    eventBasicInfo.TimeStatus = "done";
                }
                else if (wsEvent.Status && DateTime.Now < wsEvent.Start_Date)
                {
                    eventBasicInfo.TimeStatus = "income";
                }
                else
                {
                    eventBasicInfo.TimeStatus = "process";
                }
                eventBasicInfo.DonatedUser = CountDonatedUserOfEvent(eventId);
                eventBasicInfo.Likes = CountLikeInEvent(eventId);
                eventBasicInfo.NumberOfComments = CountCommentInEvent(eventId);
                eventBasicInfo.ExpectedMoney = wsEvent.ExpectedMoney;
                eventBasicInfo.RaisedMoney = raisedMoney;
                eventBasicInfo.EventType = wsEvent.EType.EventTypeName;
                eventBasicInfo.CreatedDate = wsEvent.Created_Date.ToString("dd/MM/yyyy");
                eventBasicInfo.Start_Date = wsEvent.Start_Date.ToString("dd/MM/yyyy");
                eventBasicInfo.Finish_Date = wsEvent.Finish_Date.ToString("dd/MM/yyyy");
                }   
            }
            catch (Exception)
            {
                
                //throw;
            }

            return eventBasicInfo;
        }
        public List<EventBasicInfo> GetAllEvents()
        {
            var listEvent = new List<EventBasicInfo>();
            try
            {
                List<int> IdList;
                using (var db = new Ws_DataContext())
                {
                    IdList = db.Events.Select(x => x.EventID).ToList();
                }

                using (var db = new EventDAL())
                {
                    foreach (int eventId in IdList)
                    {
                        EventBasicInfo eventBasic = db.GetFullEventBasicInformation(eventId);
                        listEvent.Add(eventBasic);
                    }
                }

            }
            catch (Exception)
            {
                //throw;
            }

            return listEvent;
        }

        public List<EventBasicInfo> GetTopNewEvent()
        {
            var topEvent = new List<EventBasicInfo>();

            try
            {
                List<int> listEventId;
                using (var db = new Ws_DataContext())
                {
                    listEventId = db.Events.OrderByDescending(x => x.Created_Date).Select(x => x.EventID).Take(10).ToList();
                }

                foreach (int userId in listEventId)
                {
                    EventBasicInfo eventBasic = GetFullEventBasicInformation(userId);
                    topEvent.Add(eventBasic);
                }

            }
            catch (Exception)
            {

                //throw;
            }
            return topEvent;
        }
        public List<EventBasicInfo> GetTopEventSortByMoneyDonateIn(int top)
        {
            List<EventBasicInfo> eventList = new List<EventBasicInfo>();
            try
            {
                List<int> eventIdList;
                using (var db = new Ws_DataContext())
                {
                    eventIdList = db.Donations.Select(x => x.EventId).Distinct().ToList();
                }

                List<EventBasicInfo> eventFullList = new List<EventBasicInfo>();
                foreach (int eventId in eventIdList)
                {
                    EventBasicInfo e = GetFullEventBasicInformation(eventId);
                    eventFullList.Add(e);
                }

                eventList = eventFullList.OrderByDescending(x => x.RaisedMoney).Take(top).ToList();
            }
            catch (Exception)
            {

                //throw;
            }

            return eventList;
        }

        public decimal GetTotalRaisedMoneyOfEvent(int eventId)
        {
            decimal raisedMoney = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    raisedMoney = db.Donations.Where(x => x.EventId == eventId).Select(x => x.DonatedMoney).Sum();
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return raisedMoney;
        }
        public int CountDonatedUserOfEvent(int eventId)
        {
            var numUser = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numUser = db.Donations.Where(x => x.EventId == eventId).Select(x => x.UserId).Distinct().Count();
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return numUser;
        }

        /// <summary>
        /// Get information about *ALL* event type using in wings
        /// </summary>
        /// <returns></returns>
        public List<EventTypeDTO> GetAllEventType()
        {
            List<EventTypeDTO> eventTypeList = new List<EventTypeDTO>();
            try
            {
                List<int> typeIdList;
                using (var db = new Ws_DataContext())
                {
                    typeIdList = db.EventTypes.Select(x=>x.EventTypeID).ToList();
                }

                foreach (var typeId in typeIdList)
                {
                    EventTypeDTO eventTypeDto = GetEventTypeInfomation(typeId);
                    eventTypeList.Add(eventTypeDto);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return eventTypeList;
        }

        /// <summary>
        /// Get information about event type using in wings
        /// </summary>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public EventTypeDTO GetEventTypeInfomation(int eventTypeId)
        {
            EventTypeDTO eventTypeDto = new EventTypeDTO();
            try
            {
                EventType eventType;
                using (var db = new Ws_DataContext())
                {
                    eventType = db.EventTypes.FirstOrDefault(x => x.EventTypeID == eventTypeId);
                }

                if (eventType!=null)
                {
                    eventTypeDto.EventTypeID = eventType.EventTypeID;
                    eventTypeDto.EventTypeName = eventType.EventTypeName;
                    eventTypeDto.Content = eventType.Content;
                    eventTypeDto.IsActive = eventType.IsActive;
                    eventTypeDto.NumberRelatedEvent = CountNumberEventRelateAnEventType(eventType.EventTypeID);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return eventTypeDto;
        }

        /// <summary>
        /// Count number of event that using *similarly* event Type
        /// </summary>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public int CountNumberEventRelateAnEventType(int eventTypeId)
        {
            int numberEventRelated = 0;

            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberEventRelated = db.Events.Where(x => x.EventType == eventTypeId).Select(x => x.EventID).Count();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberEventRelated;
        }

        public EventType UpdateEventType(EventType type)
        {
            using (var db = new Ws_DataContext())
            {
                db.EventTypes.AddOrUpdate(type);
                db.SaveChanges();

                return GetEventTypeById(type.EventTypeID);
            }
        }

        public EventType GetEventTypeById(int id)
        {
            EventType type = new EventType();
            try
            {
                using (var db = new Ws_DataContext())
                {
                    type = db.EventTypes.FirstOrDefault(x => x.EventTypeID == id);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return type;
        }
        public string GetEventNameById(int eventId)
        {

            try
            {
                string eventNm;
                using (var db = new Ws_DataContext())
                {
                    var eventGet = db.Events.FirstOrDefault(x => x.EventID == eventId);
                    if (eventGet != null)
                    {
                        eventNm = eventGet.EventName;
                    }
                    else
                    {
                        eventNm = "";
                    }
                }
                return eventNm;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public List<ListDonatorDTO> GetDonatorInEvent(int eventId)
        {
            List<ListDonatorDTO> newList = new List<ListDonatorDTO>();
            using (var db = new Ws_DataContext())
            {
                var donatorList = (from p in db.Donations
                                    where p.EventId == eventId && p.IsPublic == true
                                   select new {p.UserId, p.Ws_User.User_Information.FullName, p.DonatedDate, p.DonatedMoney, p.Content }).ToList();
                foreach(var item in donatorList)
                {
                    ListDonatorDTO getDonator = new ListDonatorDTO();
                    getDonator.UserId = item.UserId;
                    getDonator.FullName = item.FullName;
                    getDonator.DonatedMoney = item.DonatedMoney;
                    getDonator.DonatedDate = item.DonatedDate.ToString("H:mm:ss dd/MM/yy");
                    newList.Add(getDonator);
                }
                return newList;
            }

        }
		  public bool CreateEventType(EventType type)
        {
            try
            {
                using (var db = new Ws_DataContext())
                {
                    db.EventTypes.Add(type);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
        public void UpdateEvent(Event eventUpdate)
        {
            using (var db = new Ws_DataContext())
            {
                db.Events.AddOrUpdate(eventUpdate);
                db.SaveChanges();
            }
        }
        public bool DeleteComment (int CommentId, string UserName)
        {
            using (var db = new Ws_DataContext())
            {
                var currentComment = (from p in db.CommentEvents
                                     where p.CommentEventId == CommentId && p.Ws_User.UserName.Equals(UserName)
                                     select p).FirstOrDefault();
                if (currentComment != null)
                {
                    var subCommentInComment = (from p in db.SubCommentEvent
                                                      where p.CommentEventId == currentComment.CommentEventId
                                                      select p).ToList();
                    if (subCommentInComment != null)
                    {
                        foreach (var item in subCommentInComment)
                        {
                            //Delete All SubComment First
                            db.SubCommentEvent.Remove(item);
                        }
                    }
                    //Delete Comment
                    db.CommentEvents.Remove(currentComment);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public bool DeleteSubComment(int SubCommentId, string UserName)
        {
            using (var db = new Ws_DataContext())
            {
                var currentComment = (from p in db.SubCommentEvent
                                      where p.SubCommentEventId == SubCommentId && p.Ws_User.UserName.Equals(UserName)
                                      select p).FirstOrDefault();
                if (currentComment != null)
                {
                    db.SubCommentEvent.Remove(currentComment);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public void Dispose()
        {
          
        }
    }
}