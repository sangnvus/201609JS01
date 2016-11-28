using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class EventController : ApiController
    {
        [HttpGet]
        public IHttpActionResult CountLikeInEvent(int EventId)
        {
            int numberOfLikes = 0;
            using (var db = new EventDAL())
            {
                numberOfLikes = db.CountLikeInEvent(EventId);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = numberOfLikes });
        }
        [HttpGet]
        public IHttpActionResult CountLikeInCommentEvent(int CommentId)
        {
            int numberOfLikes = 0;
            using (var db = new EventDAL())
            {
                numberOfLikes = db.CountLikeInCommentEvent(CommentId);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = numberOfLikes });
        }
        [HttpGet]
        public IHttpActionResult CheckCurrentUserIsLikedOrNot(int EventId)
        {
            bool isLiked = false;
            using (var db = new EventDAL())
            {
                isLiked = db.CheckUserIsLikedOrNot(EventId,User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = isLiked });
        }
        [HttpGet]
        public IHttpActionResult ChangeLikeState(int EventId)
        {
            using (var db = new EventDAL())
            {
                var change = db.ChangelikeState(EventId, User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
        }
        [HttpGet]
        public IHttpActionResult ChangeLikeStateForComment(int CommentId)
        {
            using (var db = new EventDAL())
            {
                var change = db.ChangelikeStateForComment(CommentId, User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
        }
        [HttpGet]
        public IHttpActionResult CheckExistedSubCommentOrNot()
        {
            using (var db = new EventDAL())
            {
                var commentList = db.GetAllCommentIdAndSubCommentId();
                if (commentList == null || commentList.Count == 0)
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = commentList });
            }

        }
        //Get All Commend in current thread.
        [HttpGet]
        public IHttpActionResult GetAllComment(int eventId)
        {
            List<BasicCommentThread> commentList = new List<BasicCommentThread>();
            using (var db = new EventDAL())
            {
                if (User.Identity.IsAuthenticated) { 
                commentList = db.GetAllCommentInEvent(eventId, User.Identity.Name);
                } else commentList = db.GetAllCommentInEvent(eventId, "");
            }
            if (commentList == null || commentList.Count == 0)
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = commentList });
           

        }
        [HttpPost]
        public IHttpActionResult AddComment(AddCommentDTO comment)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            var newComment = new CommentEvent
            {
                UserId = CurrenUser,
                EventId = comment.ThreadId,
                Content = comment.CommentContent,
                Status = true,
                CommentDate = DateTime.Now
            };
            using (var db = new EventDAL())
            {
                newComment = db.AddNewComment(newComment);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = newComment });
        }
        //Add subcomment for thread to DB
        [HttpPost]
        public IHttpActionResult AddSubComment(AddSubCommentDTO comment)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            var newSubComment = new SubCommentEvent
            {
                UserId = CurrenUser,
                CommentEventId = comment.CommentThreadId,
                Content = comment.CommentContent,
                Status = true,
                CommentDate = DateTime.Now
            };
            using (var db = new EventDAL())
            {
                try
                {
                    newSubComment = db.AddNewSubComment(newSubComment);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
                catch (Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }

        }
        [HttpGet]
        public IHttpActionResult GetSubCommentByCommentId(int CommentId)
        {
            //Select All SubComment and return
            using (var db = new EventDAL())
            {

                var SubcommentList = db.GetSubCommentInEventById(CommentId, User.Identity.Name);
                if (SubcommentList == null) return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = SubcommentList });
            }
        }
        //Get All SubComment
        [HttpGet]
        public IHttpActionResult GetAllSubComment()
        {
            //Select All SubComment and return
            using (var db = new EventDAL())
            {

                var SubcommentList = db.GetAllSubCommentInEvent();
                if (SubcommentList == null) return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = SubcommentList });
            }
        }
        [HttpGet]
        public IHttpActionResult GetEventDetailById(int id)
        {
            EventBasicInfo EvtBasicInfo = new EventBasicInfo();
            try { 
                using (var db = new EventDAL()){
                EvtBasicInfo = db.GetFullEventBasicInformation(id);
                }
            }catch(Exception)
            {
                return Redirect("/#/Error");
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = EvtBasicInfo });
        }
        [HttpGet]
        public IHttpActionResult GetDonatorInEvent(int id)
        {
            List<ListDonatorDTO> list = new List<ListDonatorDTO>();
            try
            {
                using (var db = new EventDAL())
                {
                    list = db.GetDonatorInEvent(id);
                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = list });
        }
        //Get EventTimeLine       
        [HttpGet]
        public IHttpActionResult GetEventTimeLineByEventId(int id)
        {
            List <EventTimeLineBasicInfo> eventTimeLine = new List<EventTimeLineBasicInfo>();
            var EventList = new List<EventTimeLine>();
            using (var db = new EventDAL())
            {
                EventList = db.GetEventTimeLineByEventId(id);
                foreach(var eTimeLine in EventList)
                {
                    eventTimeLine.Add(new EventTimeLineBasicInfo
                    {
                        FromDate = eTimeLine.FromDate.ToString("dd/MM/yy"),
                        ToDate = eTimeLine.ToDate.ToString("dd/MM/yy"),
                        Content = eTimeLine.Content
                    });
                }
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = eventTimeLine });
        }
        //Get top view event
        [HttpGet]
        public IHttpActionResult GetTopFourEventByPoint()
        {
            List<Event> topEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            using (var db = new EventDAL())
            {
                //Get top event.
                topEvent = db.GetTopFourEventByPoint(4);
                foreach(Event e in topEvent)
                {
                    int Like = db.CountLikeInEvent(e.EventID);
                    int Comment = db.CountCommentInEvent(e.EventID);
                    //Lấy ra ảnh tương ứng với mỗi 1 event với Status = 1
                    //Note: ảnh có status bằng 1 là ảnh dùng để hiển thị trên trang Home
                    var eventMainImage = db.GetMainImageEventById(e.EventID);

                    basicEventList.Add(new EventBasicInfo
                    {
                        CreatedDate = e.Created_Date.ToString("H:mm:ss dd/MM/yy"),
                        EventID = e.EventID,
                        EventName = e.EventName,
                        Content = e.Description,
                        ShortDescription = e.ShortDescription,
                        CreatorID = e.CreatorID,
                        MainImageUrl = eventMainImage.ImageUrl,
                        Status = e.Status,
                        Likes = Like,
                        NumberOfComments = Comment
                    });
                }
            }
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= basicEventList });
        }
        //Get top 1 view event
        [HttpGet]
        [ActionName("Top1View")]
        public IHttpActionResult GetTop1ViewedEvent()
        {
            List<Event> topEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    //Get top event.
                    topEvent = db.GetTopFourEventByPoint(1);
                    foreach (Event e in topEvent)
                    {
                        //Lấy ra ảnh tương ứng với mỗi 1 event với Status = true
                        //Note: ảnh có status bằng "true" là ảnh dùng để hiển thị trên trang Event
                        var eventMainImage = db.GetMainImageEventById(e.EventID);

                        basicEventList.Add(new EventBasicInfo
                        {
                            CreatedDate = e.Created_Date.ToString("H:mm:ss dd/MM/yy"),
                            EventID = e.EventID,
                            EventName = e.EventName,
                            Content = e.Description,
                            ShortDescription = e.ShortDescription,
                            CreatorID = e.CreatorID,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Status = e.Status
                        });
                    }
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {

                return Redirect("/#/Error");
            }

        }
        // Get list event by create date
        [HttpGet]
        [ActionName("NewestEvent")]
        public IHttpActionResult GetNewestEvent()
        {
            List<Event> NewestEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    NewestEvent = db.GetNewestEventByCreatedDate();
                    foreach (Event events in NewestEvent)
                    {
                        var eventMainImage = db.GetMainImageEventById(events.EventID);
                        //get name of organizationt which is owner of this event.
                        string organizationName;
                        string eventType;
                        using (var dbOrg = new OrganizationDAL())
                        {
                            var orgOwner = dbOrg.GetOrganizationById(events.CreatorID);
                            organizationName = orgOwner.OrganizationName;
                        }
                        //get type of this event.
                        using (var dbWscontext = new Ws_DataContext())
                        {
                            var eventTypes = dbWscontext.EventTypes.Find(events.EventType);
                            eventType = eventTypes.EventTypeName;
                        }
                       
                        basicEventList.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            CreatorName = organizationName,
                            EventName = events.EventName,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            ShortDescription = events.ShortDescription,
                            Status = true,
                            EventType = eventType,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss dd/MM/yy")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }

        /// <summary>
        /// Get list event that sort by point and set them as Data to tranfer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("EventsSortByPoint")]
        public IHttpActionResult GetEventsSortByPoint()
        {
            List<Event> EventFollowPoint = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    EventFollowPoint = db.GetEventSortByPoint();
                    foreach (Event events in EventFollowPoint)
                    {
                        var eventMainImage = db.GetMainImageEventById(events.EventID);
                        //get name of organizationt which is owner of this event.
                        string organizationName;
                        string eventType;
                        using (var dbOrg = new OrganizationDAL())
                        {
                            var orgOwner = dbOrg.GetOrganizationById(events.CreatorID);
                            organizationName = orgOwner.OrganizationName;
                        }
                        //get type of this event.
                        using (var dbWscontext = new Ws_DataContext())
                        {
                            var eventTypes = dbWscontext.EventTypes.Find(events.EventType);
                            eventType = eventTypes.EventTypeName;
                        }

                        basicEventList.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            CreatorName = organizationName,
                            EventName = events.EventName,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            ShortDescription = events.ShortDescription,
                            Status = true,
                            EventType = eventType,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss dd/MM/yy")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }

        /// <summary>
        /// Get event follow type event
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetEventsFollowEventType(int typeEventId)
        {
            List<Event> EventFollowEventType = null;
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    EventFollowEventType = db.GetEventFollowEventType(typeEventId);
                    foreach (Event events in EventFollowEventType)
                    {
                        var eventMainImage = db.GetMainImageEventById(events.EventID);
                        //get name of organizationt which is owner of this event.
                        string organizationName;
                        string eventType;
                        using (var dbOrg = new OrganizationDAL())
                        {
                            var orgOwner = dbOrg.GetOrganizationById(events.CreatorID);
                            organizationName = orgOwner.OrganizationName;
                        }
                        //get type of this event.
                        using (var dbWscontext = new Ws_DataContext())
                        {
                            var eventTypes = dbWscontext.EventTypes.Find(events.EventType);
                            eventType = eventTypes.EventTypeName;
                        }

                        basicEventList.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            CreatorName = organizationName,
                            EventName = events.EventName,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            ShortDescription = events.ShortDescription,
                            Status = true,
                            EventType = eventType,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss dd/MM/yy")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = basicEventList
                });
                
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }

        }

        //Get public Message in event
        [HttpGet]
        public IHttpActionResult GetpublicMessage(int EventId)
        {
            List<MessageBasicInfoDTO> publicMessage = new List<MessageBasicInfoDTO>();
            using (var db = new EventDAL())
            {
                publicMessage = db.GetAllPublicMessage(EventId);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = publicMessage });
        }
        public IHttpActionResult GetEventListOfOrganization(int orgId)
        {
            try
            {
                List<EventBasicInfo> eventListBasicInfo;
                using (var db = new EventDAL())
                {
                    eventListBasicInfo = db.GetEventsBelongToCreator(orgId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = eventListBasicInfo
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult OrderEventListOfOrganizationByPoint(int orgId)
        {
            try
            {
                List<EventBasicInfo> eventListBasicInfo;
                using (var db = new EventDAL())
                {
                    eventListBasicInfo = db.GetEventsBelongToCreatorByPoint(orgId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = eventListBasicInfo
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult OrderEventListOfOrganizationByStatus(int orgId, bool isStatus)
        {
            try
            {
                List<EventBasicInfo> eventListBasicInfo;
                using (var db = new EventDAL())
                {
                    eventListBasicInfo = db.GetEventsBelongToCreatorByStatus(orgId, isStatus);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = eventListBasicInfo
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }

        public IHttpActionResult GetTopOneViewedEventOfOrganization(int orgId)
        {
            var basicEventList = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    //Get top event.
                    var topEvent = db.GetTopOneEventOfOrganization(orgId);
                    foreach (Event e in topEvent)
                    {
                        //Lấy ra ảnh tương ứng với mỗi 1 event với Status = true
                        //Note: ảnh có status bằng "true" là ảnh dùng để hiển thị trên trang Event
                        var eventMainImage = db.GetMainImageEventById(e.EventID);

                        basicEventList.Add(new EventBasicInfo
                        {
                            CreatedDate = e.Created_Date.ToString("H:mm:ss dd/MM/yy"),
                            EventID = e.EventID,
                            EventName = e.EventName,
                            Content = e.Description,
                            ShortDescription = e.ShortDescription,
                            CreatorID = e.CreatorID,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Status = e.Status
                        });
                    }
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicEventList });
            }
            catch (Exception)
            {

                return Redirect("/#/Error");
            }

        }

        [HttpGet]
        public IHttpActionResult GetTopEventSortByMoneyDonateIn(int top)
        {
            try
            {
                List<EventBasicInfo> eventList;

                using (var db = new EventDAL())
                {
                    eventList = db.GetTopEventSortByMoneyDonateIn(top);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = eventList
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult DeleteComment(int commentId)
        {
            using (var db = new EventDAL())
            {
                var result = db.DeleteComment(commentId, User.Identity.Name);
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data= result
                });
            }
                   
        }
        [HttpGet]
        public IHttpActionResult DeleteSubComment(int subCommentId)
        {
            using (var db = new EventDAL())
            {
                var result = db.DeleteSubComment(subCommentId, User.Identity.Name);
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = result
                });
            }

        }

        /// <summary>
        /// Get all event sort by number user donated in this event
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult EventsSortByNumberUserDonatedIn()
        {
            try
            {
                List<EventBasicInfo> basicEventList;
                using (var db = new EventDAL())
                {
                    basicEventList = db.GetEventSortByNumberOfDonator();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Data = basicEventList
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }

    }
}
