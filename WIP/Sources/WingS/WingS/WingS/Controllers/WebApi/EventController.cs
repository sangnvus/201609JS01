﻿using System;
using System.Collections.Generic;
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
        public IHttpActionResult CheckCurrentUserIsLikedOrNot(int EventId)
        {
            bool isLiked = false;
            using (var db = new EventDAL())
            {
                isLiked = db.CheckUserIsLikedOrNot(EventId);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = isLiked });
        }
        [HttpGet]
        public IHttpActionResult ChangeLikeState(int EventId)
        {
            using (var db = new EventDAL())
            {
                var change = db.ChangelikeState(EventId);
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
            using (var db = new EventDAL())
            {
                var commentList = db.GetAllCommentInEvent(eventId);
                if (commentList == null || commentList.Count == 0)
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = commentList });
            }

        }
        [HttpPost]
        public IHttpActionResult AddComment(AddCommentDTO comment)
        {
            var newComment = new CommentEvent
            {
                UserId = WsConstant.CurrentUser.UserId,
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
            var newSubComment = new SubCommentEvent
            {
                UserId = WsConstant.CurrentUser.UserId,
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

                var SubcommentList = db.GetSubCommentInEventById(CommentId);
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
                EvtBasicInfo = db.GetEventBasicInfoById(id);
                }
            }catch(Exception)
            {
                return Redirect("/#/Error");
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = EvtBasicInfo });
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
                        Status = e.Status
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
                        basicEventList.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            EventName = events.EventName,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            ShortDescription = events.ShortDescription,
                            Status = true,
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

        public IHttpActionResult GetEventListOfOrganization(int orgId)
        {
            var eventListBasicInfo = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    var eventList = db.GetEventsBelongToCreator(orgId);

                    foreach (Event events in eventList)
                    {
                        var eventMainImage = db.GetMainImageEventById(events.EventID);
                        eventListBasicInfo.Add(new EventBasicInfo
                        {
                            EventID = events.EventID,
                            CreatorID = events.CreatorID,
                            EventName = events.EventName,
                            MainImageUrl = eventMainImage.ImageUrl,
                            Content = events.Description,
                            ShortDescription = events.ShortDescription,
                            Status = true,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss dd/MM/yy")
                        });
                    }
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
        

    }
}
