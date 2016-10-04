using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class ThreadController : ApiController
    {
        // Get 4 Thread có View lớn nhiều nhất
        [HttpGet]
        public IHttpActionResult GetTopFourThread()
        {
            List<Thread> topFourThread = null;
            var basicThreadList = new List<ThreadBasicInfo>();
            try
            {
                using (var db = new ThreadDAL())
                {
                    topFourThread = db.GetTopThreadByView(4);
                    foreach (Thread thread in topFourThread)
                    {
                        var threadMainImage = db.GetMainImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadMainImage.ImageUrl,
                            Content = thread.Content,   
                            Likes = thread.Likes,
                            Views = thread.Views,
                            Status = true,
                            CreatedDate = thread.CreatedDate.ToString("H:mm:ss MM/dd/yy")

                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicThreadList });
            }
            catch (Exception)
            {

               // ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
           
        }
		
        [HttpPost]
        public IHttpActionResult GetThreadById(int Id)
        {
            try { 
            using (var db = new ThreadDAL())
            {
                Thread current = db.GetThreadById(Id);
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = current });
                }
               
            }
            catch(Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR});
            }
        }
        // Get list thread by create date
        [HttpGet]
        [ActionName("NewestThread")]
        public IHttpActionResult GetNewestThread()
        {
            List<Thread> NewestThread = null;
            var basicThreadList = new List<ThreadBasicInfo>();
            try
            {
                using (var db = new ThreadDAL())
                {
                    NewestThread = db.GetNewestThreadByCreatedDate();
                    foreach (Thread thread in NewestThread)
                    {
                        var threadMainImage = db.GetMainImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadMainImage.ImageUrl,
                            Content = thread.Content,
                            Likes = thread.Likes,
                            Views = thread.Views,
                            Status = true,
                            CreatedDate = DateTime.Now.ToString("H:mm:ss MM/dd/yy")
                        });
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicThreadList });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }
    }
}
