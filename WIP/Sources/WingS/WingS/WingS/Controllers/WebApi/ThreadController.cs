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
                    var threadMainImage = db.GetMainImageThreadById(thread.ThreadId);
                    basicThreadList.Add(new ThreadBasicInfo
                    {
                        ThreadID = thread.ThreadId,
                        UserID = thread.UserId,
                        ThreadName = thread.Title,
                        ImageUrl = threadMainImage.ImageUrl,
                        Content = thread.Content,
                        Status = true
                    });
                }
            }

            return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.SUCCESS, Data = basicThreadList});

        }

        [HttpPost]
        public IHttpActionResult CreateDiscussion(CreateThreadInfo thread, HttpPostedFileBase[] Images)
        {
            Thread newThread = null;
            //Add thread to DB
            using (var db = new ThreadDAL())
            {
                newThread=db.AddNewThread(thread);
            }
            //Add Imgaes of Discussion to server
            try
            {
                foreach (HttpPostedFileBase img in Images)
                {
                    //rebuild imgae name
                    string imageName = WsConstant.randomString() + Path.GetExtension(img.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload/ThreadImage"), imageName);
                    img.SaveAs(path);
                    string imgaeUrl = "Upload/ThreadImage" + imageName;
                    //Add Image to db.
                    using (var db = new AlbumImageDAL())
                    {
                        db.AddNewAlbum(new ThreadAlbumImageDTO(newThread.ThreadId, imgaeUrl));
                    }

                }
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }

            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
        }
    }
}
