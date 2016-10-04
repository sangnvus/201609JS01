﻿using System;
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
                        List<String> threadImage = db.GetAllImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadImage,
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

        [HttpGet]
        public IHttpActionResult GetThreadById(int id)
        {
            try
            {
                using (var db = new ThreadDAL())
                {
                    Thread current = db.GetThreadById(id);
                    ThreadBasicInfo threadBasic = new ThreadBasicInfo();

                    threadBasic.ThreadID = current.ThreadId;
                    threadBasic.UserID = current.UserId;
                    threadBasic.ThreadName = current.Title;
                    threadBasic.ImageUrl = db.GetAllImageThreadById(id);
                    threadBasic.Content = current.Content;
                    threadBasic.Likes = current.Likes;
                    threadBasic.Views = current.Views;
                    threadBasic.Status = current.Status;
                    threadBasic.CreatedDate = current.CreatedDate.ToString("H:mm:ss MM/dd/yy");



                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = threadBasic });
                }

            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
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
                            ImageUrl = null,//threadMainImage.ImageUrl,
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
