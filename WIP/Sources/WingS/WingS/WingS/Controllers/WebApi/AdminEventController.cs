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
                    Status = WsConstant.HttpMessageType.ERROR
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
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
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
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
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
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
        }
    }
}
