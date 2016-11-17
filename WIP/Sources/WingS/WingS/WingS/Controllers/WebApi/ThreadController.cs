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
        public IHttpActionResult CheckCurrentUserIsLikedOrNot(int ThreadId)
        {
            bool isLiked = false;
            using (var db = new ThreadDAL())
            {
                isLiked = db.CheckUserIsLikedOrNot(ThreadId, User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = isLiked });
        }
        [HttpGet]
        public IHttpActionResult CountLikeInThread(int ThreadId)
        {
            int numberOfLikes = 0;
            using (var db = new ThreadDAL())
            {
                numberOfLikes = db.CountLikeInThread(ThreadId);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = numberOfLikes });
        }
        [HttpGet]
        public IHttpActionResult ChangeLikeState(int ThreadId)
        {
            using (var db = new ThreadDAL())
            {
                var change = db.ChangelikeState(ThreadId, User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
        }
        [HttpGet]
        public IHttpActionResult CheckExistedSubCommentOrNot()
        {
            using (var db = new ThreadDAL())
            {
                var commentList = db.GetAllCommentIdAndSubCommentId();
                if (commentList == null || commentList.Count == 0)
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = commentList });
            }

        }
        //Get All Commend in current thread.
        [HttpGet]
        public IHttpActionResult GetAllComment(int threadId)
        {
            using (var db = new ThreadDAL())
            {
               var commentList = db.GetAllCommentInThread(threadId);
                if (commentList == null||commentList.Count==0)
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data= commentList });
            }
           
        }
        //Add comment to db
        [HttpPost]
        public IHttpActionResult AddComment(AddCommentDTO comment )
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            var newComment = new CommentThread
            {
                UserId=CurrenUser,
                ThreadId = comment.ThreadId,
                Content = comment.CommentContent,
                Status = true,
                CommentDate = DateTime.Now
            };
            using (var db = new ThreadDAL())
            {
                newComment = db.AddNewComment(newComment);
            }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = newComment });
        }
        //Add subcomment for thread to DB
        [HttpPost]
        public IHttpActionResult AddSubComment(AddSubCommentDTO comment)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            var newSubComment = new SubCommentThread
            {
                UserId = CurrenUser,
                CommentThreadId = comment.CommentThreadId,
                Content = comment.CommentContent,
                Status = true,
                CommentDate = DateTime.Now
            };
            using (var db = new ThreadDAL())
            {
                try { 
                newSubComment = db.AddNewSubComment(newSubComment);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS});
                }
                catch(Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }
           
        }
        [HttpGet]
        public IHttpActionResult GetSubCommentByCommentId(int CommentId)
        {
            //Select All SubComment and return
            using (var db = new ThreadDAL())
            {

                var SubcommentList = db.GetSubCommentInThreadById(CommentId);
                if (SubcommentList == null) return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = SubcommentList });
            }
        }
        //Get All SubComment
        [HttpGet]
        public IHttpActionResult GetAllSubComment()
        {
            //Select All SubComment and return
            using (var db = new ThreadDAL())
            {
                
                    var SubcommentList = db.GetAllSubCommentInThread();
                    if (SubcommentList == null) return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
                    else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = SubcommentList });
            }
        }
        // Get 4 Thread có View lớn nhiều nhất
        [HttpGet]
        public IHttpActionResult GetTopThreadByCreatedDate()
        {
            List<Thread> topFourThread = null;
            var basicThreadList = new List<ThreadBasicInfo>();
            try
            {
                using (var db = new ThreadDAL())
                {
                    topFourThread = db.GetTopThreadByCreatedDate(4);
                    foreach (Thread thread in topFourThread)
                    {
                        int Like = db.CountLikeInThread(thread.ThreadId);
                        int Comment = db.CountCommentInThread(thread.ThreadId);
                        List<String> threadImage = db.GetAllImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadImage,
                            Content = thread.Content,   
                            ShortDescription = thread.ShortDescription,
                            Likes = Like,
                            Comments = Comment,
                            Status = true,
                            CreatedDate = thread.CreatedDate.ToString("H:mm:ss dd/MM/yy"),

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

        /// <summary>
        /// Get Baisc Thread using  Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                    threadBasic.Status = current.Status;
                    threadBasic.CreatedDate = current.CreatedDate.ToString("H:mm:ss | MM/dd/yyyy");
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = threadBasic });
                }

            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }

        /// <summary>
        /// Get Basic user who creat this thread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserCreatedThread(int id)
        {
            try
            {
                using (var db = new UserDAL())
                {
                    Ws_User wsUser = db.GetUserById(id);
                    User_Information userInformation = db.GetUserInformation(id);
                    UserBasicInfoDTO user = new UserBasicInfoDTO();
                    user.UserName = wsUser.UserName;
                    user.AccountType = wsUser.AccountType;
                    user.IsActive = wsUser.IsActive;
                    user.FullName = userInformation.FullName==null? wsUser.UserName: userInformation.FullName;
                    user.ProfileImage = userInformation.ProfileImage;
                    user.Email = wsUser.Email;

                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = user });
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
                        List<String> threadImage = db.GetAllImageThreadById(thread.ThreadId);
                        basicThreadList.Add(new ThreadBasicInfo
                        {
                            ThreadID = thread.ThreadId,
                            UserID = thread.UserId,
                            ThreadName = thread.Title,
                            ImageUrl = threadImage,
                            Content = thread.Content,
                            ShortDescription =  thread.ShortDescription,
                            Status = true,
                            CreatedDate = thread.CreatedDate.ToString("H:mm:ss MM/dd/yy")
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
