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
    public class ThreadController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTopFourThread()
        {
            List<Thread> topFourThread = null;
            var basicThreadList = new List<ThreadBasicInfo>();

            using (var db = new ThreadDAL())
            {
                topFourThread = db.GetTopThreadByView(4);
                foreach (Thread thread in topFourThread)
                {
                    basicThreadList.Add(new ThreadBasicInfo
                    {
                        ThreadID = thread.ThreadId,
                        UserID = thread.UserId,
                        ThreadName = thread.Title,
                        ImageUrl = "",
                        Content = thread.Content,
                        Status = true
                    });
                }
            }

            return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.SUCCESS, Data = basicThreadList});

        }
    }
}
