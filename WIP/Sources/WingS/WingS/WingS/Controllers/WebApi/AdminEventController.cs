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
        public IHttpActionResult GetTopHotEvent()
        {
            List<EventBasicInfo> listEvent = new List<EventBasicInfo>();
            try
            {
                using (var db = new EventDAL())
                {
                    listEvent = db.GetTopHotEvent();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listEvent });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot Get Top Hot Event",
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
                    Message = "Get Thread with ID Successfully",
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
    }
}
