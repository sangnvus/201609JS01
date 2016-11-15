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
        /// Get user manage basisc information
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
    }
}
