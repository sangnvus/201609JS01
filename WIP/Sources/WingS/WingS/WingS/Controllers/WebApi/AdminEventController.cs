using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class AdminEventController : ApiController
    {
        /// <summary>
        /// Get event info for cirlcle tile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetEventManageBasicInfor()
        {
            try
            {
                EventCircleTileDTO circleInfor = new EventCircleTileDTO();
                using (var db = new EventDAL())
                {
                    circleInfor = db.GetEventCircleTile();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Data = circleInfor
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Event info for Circle Tiles",
                    Type = ""
                });
            }
        }
        /// <summary>
        /// Get Top New Event
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopNewEvent()
        {
            List<EventBasicInfo> listEvent = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    listEvent = db.GetTopNewEvent();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listEvent });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Top New Event",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult GetTopDonatedEvent()
        {
            List<EventBasicInfo> listEvent = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    listEvent = db.GetTopEventSortByMoneyDonateIn(10);
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listEvent });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Top Donated Event",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult GetAllEvent()
        {
            List<EventBasicInfo> listEvent = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    listEvent = db.GetAllEvents();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listEvent });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Event for Event List Page",
                    Type = ""
                });
            }
        }
        [HttpGet]
        public IHttpActionResult GetEventWithId(int eventId)
        {
            try
            {
                EventBasicInfo currentThread;

                using (var db = new EventDAL())
                {
                    currentThread = db.GetFullEventBasicInformation(eventId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get Event with ID Successfully",
                    Type = "",
                    Data = currentThread
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Event with ID",
                    Type = ""
                });
            }
        }

        /// <summary>
        /// Get all type event in EventType Table
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllEventType()
        {
            try
            {
                List<EventTypeDTO> listEventType;

                using (var db = new EventDAL())
                {
                    listEventType = db.GetAllEventType();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get all event type successfully",
                    Type = "",
                    Data = listEventType
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot get event type",
                    Type = ""
                });
            }
        }

        /// <summary>
        /// Change status of eventType
        /// </summary>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ChangeSatusOfEventType(int eventTypeId)
        {
            try
            {
                bool eventTypeStatus;

                using (var db = new EventDAL())
                {
                    var eventType = db.GetEventTypeById(eventTypeId);
                    eventType.IsActive = !eventType.IsActive;
                    eventTypeStatus = eventType.IsActive;
                    db.UpdateEventType(eventType);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Change status of eventType successfully",
                    Type = "",
                    Data = eventTypeStatus
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot change status of eventType",
                    Type = ""
                });
            }
        }

        
        [HttpPost]
        public IHttpActionResult CreateEventType(EventType eventType)
        {
            try
            {
                bool createStatus;

                eventType.IsActive = true;

                using (var db = new EventDAL())
                {
                    createStatus = db.CreateEventType(eventType);
                }
                   

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Change status of eventType successfully",
                    Type = "",
                    Data = createStatus
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot change status of eventType",
                    Type = ""
                });
            }
        }
		[HttpGet]
        [ActionName("ChangeStatusEvent")]
        public IHttpActionResult ChangeStatusEvent(int eventId)
        {
            try
            {
                string timeStatusEvent;
                using (var db = new EventDAL())
                {
                    var eventGet = db.GetEventById(eventId);
                    eventGet.Status = !eventGet.Status;

                    if (!eventGet.Status)
                    {
                        timeStatusEvent = "ban";
                    }
                    else if (eventGet.Status && DateTime.Now > eventGet.Finish_Date)
                    {
                        timeStatusEvent = "done";
                    }
                    else if (eventGet.Status && DateTime.Now < eventGet.Start_Date)
                    {
                        timeStatusEvent = "income";
                    }
                    else
                    {
                        timeStatusEvent = "process";
                    }
                    db.UpdateEvent(eventGet);
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = timeStatusEvent });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
        //Get EventTimeLine       
        [HttpGet]
        public IHttpActionResult GetEventTimeLineByEventId(int eventId)
        {
            List<EventTimeLineBasicInfo> eventTimeLine = new List<EventTimeLineBasicInfo>();
            var EventList = new List<EventTimeLine>();
            using (var db = new EventDAL())
            {
                EventList = db.GetEventTimeLineByEventId(eventId);
                foreach (var eTimeLine in EventList)
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
    }
}
