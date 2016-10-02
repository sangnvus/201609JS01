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
    public class Thread1Controller : ApiController
    {
        // Get danh sách các Thread (8 cái mới nhất)
        [HttpGet]
        public IHttpActionResult GetEightNewestThread()
        {
            List<Thread> eightNewestThread = null;
            var basicThreadList = new List<ThreadBasicInfo>();
            try
            {
                using (var db = new ThreadDAL())
                {
                    eightNewestThread = db.GetNewestThreadByCreatedDate(8);
                    foreach (Thread thread in eightNewestThread)
                    {
                        var threadMainImage = db.GetMainImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadMainImage.ImageUrl,
                            Content = thread.Content,
                            Likes= thread.Likes,
                            Views= thread.Views,
                            Status = true,
                            CreatedDate = DateTime.Now.ToString("MM / dd / yy H: mm:ss")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicThreadList });
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }

    }
}