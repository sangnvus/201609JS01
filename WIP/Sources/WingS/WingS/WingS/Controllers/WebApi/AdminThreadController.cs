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
    public class AdminThreadController : ApiController
    {
        /// <summary>
        /// Get user manage basisc information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetThreadManageBasicInfor()
        {
            try
            {
                ThreadManageBasicInforDTO threadManageInfor = new ThreadManageBasicInforDTO();

                using (var db = new ThreadDAL())
                {
                    threadManageInfor.NumberBanThread = db.CountThreadIsBan();
                    threadManageInfor.NumberNewThread = db.CountNewThread();
                    threadManageInfor.NumberTotalthread = db.CountTotalThread();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = threadManageInfor
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
        /// <summary>
        /// Get 10 newest thread in list newest Thread
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get10NewestThread()
        {
            try
            {
                var newestThread = new List<ThreadBasicInfo>();


                using (var db = new ThreadDAL())
                {

                    newestThread = db.GetNewestNumberThread(10);

                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = newestThread
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
        /// <summary>
        /// Get top Thread have Like is most
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopLikeThread()
        {
            try
            {
                var topLikeThread = new List<ThreadBasicInfo>();


                using (var db = new ThreadDAL())
                {

                    topLikeThread = db.GetTopLikeNumberThread(5);

                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = topLikeThread
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
        // Get all Thread in Thread table
        [HttpGet]
        [ActionName("GetAllThread")]
        public IHttpActionResult GetAllThread()
        {
            List<ThreadBasicInfo> listThread = new List<ThreadBasicInfo>();
            try
            {
                using (var db = new ThreadDAL())
                {
                    listThread = db.GetAllThread();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listThread });
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
        /// <summary>
        /// Get thread using id
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetThreadWithId")]
        public IHttpActionResult GetThreadWithId(int threadId)
        {
            try
            {
                ThreadBasicInfo currentThread;

                using (var db = new ThreadDAL())
                {
                    currentThread = db.GetFullThreadBasicInformation(threadId);
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
                    Message = "Cannot Get Thread with ID",
                    Type = ""
                });
            }
        }
        /// <summary>
        /// Change status of thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ChangeStatusThread")]
        public IHttpActionResult ChangeStatusThread(int threadId)
        {
            try
            {
                bool statusThread;
                using (var threadDal = new ThreadDAL())
                {
                    var thread = threadDal.GetThreadById(threadId);
                    thread.Status = !thread.Status;
                    statusThread = thread.Status;
                    threadDal.UpdateThread(thread);
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = statusThread });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
    }
}
