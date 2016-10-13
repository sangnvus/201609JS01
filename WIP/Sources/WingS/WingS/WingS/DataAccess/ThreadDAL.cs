using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class ThreadDAL : IDisposable
    {
        public int CountLikeInThread(int ThreadId)
        {
            using (var db = new Ws_DataContext())
            {
                int CountLike = db.LikeThreads.OrderByDescending(x => x.ThreadId == ThreadId && x.Status == true).Count();
                return CountLike;
            }

        }
        public bool CheckUserIsLikedOrNot(int ThreadId)
        {
            using (var db = new Ws_DataContext())
            {
                var isLike = db.LikeThreads.OrderByDescending(x => x.ThreadId == ThreadId && x.UserId == WsConstant.CurrentUser.UserId && x.Status == true);
                if (isLike != null) return true;
                else return false;
            }
        }
        public bool ChangelikeState(int ThreadId)
        {
            try
            {
                using (var db = new Ws_DataContext())
                {
                    //Check current like status.
                    LikeThreads current = (
                                           from p in db.LikeThreads
                                           where p.ThreadId == ThreadId && p.UserId == WsConstant.CurrentUser.UserId
                                           select p
                                           ).SingleOrDefault();
                    if (current != null)
                    {
                        current.Status = !current.Status;
                    }
                    else
                    {
                        db.LikeThreads.Add(new LikeThreads() { ThreadId = ThreadId, UserId = WsConstant.CurrentUser.UserId, Status = true });
                    }
                    db.SaveChanges();
                    return true;

                }
            }
            catch (Exception) { return false; }
        }
        public List<int> GetAllCommentIdAndSubCommentId()
        {
            List<int> list = new List<int>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentThread
                        .Where(x => x.Status == true)
                        .Select(x => new {x.CommentThreadId });
                    foreach (var item in listComment)
                    {
                        list.Add(item.CommentThreadId);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public List<BasicCommentThread> GetAllCommentInThread (int threadId)
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.CommentThreads
                        .Where(x=>x.Status==true&&x.ThreadId==threadId)
                        .Select(x=> new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage,x.CommentThreadId, x.Content, x.CommentDate})
                        .OrderByDescending(x=>x.CommentDate).ToList();
                    foreach(var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.CommentThreadId;
                        bs.Content = item.Content;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss MM/dd/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }
                   
                }
                catch (Exception) { return null; }
                
            }
            return list;
        }
        public List<BasicCommentThread> GetSubCommentInThreadById(int CommentId)
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentThread
                        .Where(x => x.Status == true&&x.CommentThreadId==CommentId)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.CommentThreadId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = CommentId;
                        bs.Content = item.Content;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss MM/dd/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public List<BasicCommentThread> GetAllSubCommentInThread()
        {
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentThread
                        .Where(x => x.Status == true)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.CommentThreadId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.CommentThreadId;
                        bs.Content = item.Content;
                        if (DateTime.Now.Subtract(item.CommentDate).Hours <= 24 && DateTime.Now.Subtract(item.CommentDate).Hours >= 1)
                            bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Hours + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss MM/dd/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        list.Add(bs);
                    }

                }
                catch (Exception) { return null; }

            }
            return list;
        }
        public SubCommentThread AddNewSubComment(SubCommentThread subComment)
        {
            using (var db = new Ws_DataContext())
            {
                var newComment = db.SubCommentThread.Add(subComment);
                db.SaveChanges();
                return newComment;
            }

        }
        public CommentThread AddNewComment(CommentThread comment)
        {
            using (var db = new Ws_DataContext())
            {
                var newComment = db.CommentThreads.Add(comment);
                db.SaveChanges();
                return newComment;
            }
          
        }
        public List<Thread> GetTopThreadByCreatedDate(int threadNumber)
        {
            List<Thread> listThreads = null;

            using (var db = new Ws_DataContext())
            {
                var topThread = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x=>x.Status== true).Take(threadNumber);
                listThreads = topThread.ToList();
            }

            return listThreads;
        }
        // Get Thread by Created
        public List<Thread> GetNewestThreadByCreatedDate()
        {
            List<Thread> listThreads = null;
            //Get All thread by created Date
            using (var db = new Ws_DataContext())
            {
                var newestThread = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true);
                listThreads = newestThread.ToList();
            }

            return listThreads;
        }
        public Thread GetThreadById(int threadId)
        {
            using (var db = new Ws_DataContext())
            {
                var currentThread = db.Threads.FirstOrDefault(x => x.ThreadId == threadId);
                return currentThread;
            }
          
        }
        public Thread AddNewThread(CreateThreadInfo thread)
        {
            var newThread = CreateEmptyThread();
            newThread.UserId = WsConstant.CurrentUser.UserId;
            newThread.Title = thread.Title;
            newThread.Content = thread.Content;
            using (var db = new Ws_DataContext())
                 {
                     db.Threads.Add(newThread);
                     db.SaveChanges();
                     return GetThreadById(newThread.ThreadId);
                 }

        }
        public Thread CreateEmptyThread()
        {
            using (var db = new Ws_DataContext())
            {
                var thread = db.Threads.Create();
                thread.UserId = 0;
                thread.Title = "";
                thread.Content = "";
                thread.VideoUrl = "";
                thread.CreatedDate = DateTime.Now;
                thread.UpdatedDate = DateTime.Now;
                thread.Status = true;
                return thread;
            }
        }

        /// <summary>
        /// Get all image of an Thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public List<String> GetAllImageThreadById(int threadId)
        {
            List<ThreadAlbumImage> threadImagesList;
            List<String> threadImagesUrlList = new List<string>();

            using (var db = new Ws_DataContext())
            {
                var imageList = db.ThreadAlbum.Where(x => x.ThreadId == threadId);
                threadImagesList = imageList.ToList();
            }

            foreach (ThreadAlbumImage threadImage in threadImagesList)
            {
                String url = threadImage.ImageUrl;
                threadImagesUrlList.Add(url);
            }

            return threadImagesUrlList;
        }

        /// <summary>
        /// Get main image of an Thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public ThreadAlbumImage GetMainImageThreadById(int threadId)
        {
            ThreadAlbumImage threadMainImage;

            using (var db = new Ws_DataContext())
            {
                var image = db.ThreadAlbum.FirstOrDefault(x => x.ThreadId == threadId && x.status == true);
                threadMainImage = image;
            }

            return threadMainImage;
        }
        public int GetNumberOfPostPerUser(string userName)
        {
            using (var db = new Ws_DataContext())
            {
                int numberOfPost = db.Threads.Where(x => x.Ws_User.UserName.Equals(userName)).Count();
                return numberOfPost;                
            }
        }

        public void Dispose()
        {
            
        }
    }
}