using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                int CountLike = db.LikeThreads.Count(x => x.ThreadId == ThreadId && x.Status == true);
                return CountLike;
            }

        }
        public bool CheckUserIsLikedOrNot(int ThreadId, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            using (var db = new Ws_DataContext())
            {
                var isLike = (from p in db.LikeThreads
                              where p.ThreadId == ThreadId && p.UserId == CurrenUser && p.Status == true
                              select p).SingleOrDefault();
                if (isLike != null) return true;
                else return false;
            }
        }
        // Update Thread
        public Thread UpdateThread(Thread thread)
        {
            try
            {
                using (var db = new Ws_DataContext())
                {
                    db.Threads.AddOrUpdate(thread);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return thread;
        }
        public bool ChangelikeState(int ThreadId, string UserName)
        {
            try
            {
                int CurrenUser = 0;
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
                }
                using (var db = new Ws_DataContext())
                {
                    //Check current like status.
                    LikeThreads current = (
                                           from p in db.LikeThreads
                                           where p.ThreadId == ThreadId && p.UserId == CurrenUser
                                           select p
                                           ).SingleOrDefault();
                    if (current != null)
                    {
                        if (current.Status == true) current.Status = false;
                        else current.Status = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.LikeThreads.Add(new LikeThreads() { ThreadId = ThreadId, UserId = CurrenUser, Status = true });
                        db.SaveChanges();
                    }
                    
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
        public List<BasicCommentThread> GetAllCommentInThread (int threadId, string currentUserName)
        {
            int CurrenUser = 0;
            if (currentUserName != "")
            {
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(currentUserName).UserID;
                }
            }
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.CommentThreads
                        .Where(x=>x.Status==true&&x.ThreadId==threadId)
                        .Select(x=> new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage,x.CommentThreadId, x.Content, x.CommentDate, x.SubComment.Count})
                        .OrderByDescending(x=>x.CommentDate).ToList();
                    foreach(var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.CommentThreadId;
                        bs.Content = item.Content;
                        if (currentUserName != "")
                        {
                            if (db.LikeCommentThreads.Where(x => x.CommentId == item.CommentThreadId && x.UserId == CurrenUser && x.Status == true).SingleOrDefault() != null)
                            {
                                bs.isLiked = true;
                            }
                        }
                        else bs.isLiked = false;
                        if (CurrenUser == item.UserId)
                        {
                            bs.isDeleted = true;
                        }
                        else bs.isDeleted = false;
                        bs.NumberSubComment = item.Count;
                        if (DateTime.Now.Subtract(item.CommentDate).TotalHours <= 24 && DateTime.Now.Subtract(item.CommentDate).TotalHours >= 1)
                            bs.CommentedTime = Math.Round(DateTime.Now.Subtract(item.CommentDate).TotalHours,1) + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).TotalHours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
                        else bs.CommentedTime = DateTime.Now.Subtract(item.CommentDate).Minutes + " Phút cách đây";
                        bs.NumberOfLikes = db.LikeCommentThreads.Where(x => x.CommentId == item.CommentThreadId && x.Status == true).Count();
                        list.Add(bs);
                    }
                   
                }
                catch (Exception) { return null; }
                
            }
            return list;
        }
        public List<BasicCommentThread> GetSubCommentInThreadById(int CommentId, string currentUserName)
        {
            int CurrenUser = 0;
            if (currentUserName != "")
            {
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(currentUserName).UserID;
                }
            }
            List<BasicCommentThread> list = new List<BasicCommentThread>();
            using (var db = new Ws_DataContext())
            {
                try
                {
                    var listComment = db.SubCommentThread
                        .Where(x => x.Status == true&&x.CommentThreadId==CommentId)
                        .Select(x => new { x.UserId, x.Ws_User.UserName, x.Ws_User.User_Information.ProfileImage, x.SubCommentThreadId, x.Content, x.CommentDate })
                        .OrderByDescending(x => x.CommentDate).ToList();
                    foreach (var item in listComment)
                    {
                        BasicCommentThread bs = new BasicCommentThread();
                        bs.UserCommentedId = item.UserId;
                        bs.UserCommentedName = item.UserName;
                        bs.UserImageProfile = item.ProfileImage;
                        bs.CommentId = item.SubCommentThreadId;
                        bs.Content = item.Content;
                        if (CurrenUser == item.UserId)
                        {
                            bs.isDeleted = true;
                        }
                        else bs.isDeleted = false;
                        if (DateTime.Now.Subtract(item.CommentDate).TotalHours <= 24 && DateTime.Now.Subtract(item.CommentDate).TotalHours >= 1)
                            bs.CommentedTime = Math.Round(DateTime.Now.Subtract(item.CommentDate).TotalHours,1) + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).TotalHours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
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
                        if (DateTime.Now.Subtract(item.CommentDate).TotalHours <= 24 && DateTime.Now.Subtract(item.CommentDate).TotalHours >= 1)
                            bs.CommentedTime = Math.Round(DateTime.Now.Subtract(item.CommentDate).TotalHours,1) + " Tiếng cách đây";
                        else if (DateTime.Now.Subtract(item.CommentDate).Hours > 24)
                            bs.CommentedTime = item.CommentDate.ToString("H:mm:ss dd/MM/yy");
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
        public List<ThreadBasicInfo> GetNewestThreadByCreatedDate()
        {
            var listThread = new List<ThreadBasicInfo>();
            try
            {
                List<int> userIdThread;
                using (var db = new Ws_DataContext())
                {
                    userIdThread = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).Select(x => x.ThreadId).ToList();
                }

                using (var db = new ThreadDAL())
                {
                    foreach (int threadId in userIdThread)
                    {
                        ThreadBasicInfo threadBasic = db.GetFullThreadBasicInformation(threadId);
                        listThread.Add(threadBasic);
                    }
                }

            }
            catch (Exception)
            {
                //throw;
            }
            return listThread;
        }
        public Thread GetThreadById(int threadId)
        {
            using (var db = new Ws_DataContext())
            {
                var currentThread = db.Threads.FirstOrDefault(x => x.ThreadId == threadId);
                return currentThread;
            }
          
        }
        public Thread AddNewThread(CreateThreadInfo thread, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            var newThread = CreateEmptyThread();
            newThread.UserId = CurrenUser;
            newThread.Title = thread.Title;
            newThread.Etitle = ConvertToUnSign.Convert(thread.Title);
            newThread.Content = thread.Content;
            newThread.ShortDescription = thread.ShortDescription;
            using (var db = new Ws_DataContext())
                 {
                     db.Threads.Add(newThread);
                     db.SaveChanges();
                     return GetThreadById(newThread.ThreadId);
                 }

        }
        public Thread UpdateThread(CreateThreadInfo thread, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            using (var db = new Ws_DataContext())
            {
                //Get current Thread To Edit
                var currentThread = db.Threads.Where(x => x.ThreadId == thread.ThreadId).SingleOrDefault();
                currentThread.Title = thread.Title;
                currentThread.Content = thread.Content;
                currentThread.ShortDescription = thread.ShortDescription;
                currentThread.UpdatedDate = DateTime.Now;
                if (CurrenUser != currentThread.UserId)
                {
                    return null;
                }
                else
                {
                    db.Threads.AddOrUpdate(currentThread);
                    db.SaveChanges();
                }
                return currentThread;

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
        public int GetNumberOfPostPerUser(int userId)
        {
            int numberOfPost = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberOfPost = db.Threads.Count(x => x.UserId.Equals(userId));
                }
            }
            catch (Exception)
            {
                return 0;
                //throw;
            }
            return numberOfPost;
        }
        /// <summary>
        /// count total threads
        /// </summary>
        /// <returns></returns>
        public int CountTotalThread()
        {
            using (var db = new Ws_DataContext())
            {
                var numberThread = db.Threads.Count();
                return numberThread;
            }
        }
        /// <summary>
        /// Count number of new Thread which have been created less than 1 day
        /// </summary>
        /// <returns></returns>
        public int CountNewThread()
        {
            using (var db = new Ws_DataContext())
            {
                DateTime dateBeforeThreeDay = DateTime.UtcNow.AddDays(-1);
                var numberThread = db.Threads.Count(x => x.CreatedDate >= dateBeforeThreeDay);
                return numberThread;
            }
        }
        /// <summary>
        /// Count number of user is actived or not
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public int CountThreadIsBan()
        {
            using (var db = new Ws_DataContext())
            {
                var numberThread = db.Threads.Count(x => x.Status == false);
                return numberThread;
            }
        }
        /// <summary>
        /// Get Thread have been create less than 1 day
        /// </summary>
        /// <returns></returns>
        public List<Thread> GetNewThread()
        {
            List<Thread> listThread;
            using (var db = new Ws_DataContext())
            {
                DateTime dateBeforeThreeDay = DateTime.UtcNow.AddDays(-1);
                var threads = db.Threads.OrderByDescending(x => x.CreatedDate).Where(x => x.CreatedDate >= dateBeforeThreeDay);
                listThread = threads.ToList();
            }
            return listThread;
        }
        /// <summary>
        /// Get list newest thread 
        /// </summary>
        /// <returns></returns>
        public List<ThreadBasicInfo> GetNewestNumberThread(int top)
        {
            List<ThreadBasicInfo> topNewestThread = new List<ThreadBasicInfo>();

            try
            {
                var threadListBasic = new List<ThreadBasicInfo>();
                using (var db = new ThreadDAL())
                {
                    List<Thread> threadList = db.GetNewThread();
                    if (threadList != null)
                    {
                        foreach (Thread thread in threadList)
                        {
                            //Get Creator of Thread
                            Ws_User creatorThread;
                            using (var dbUser = new UserDAL())
                            {
                                creatorThread = dbUser.GetUserById(thread.UserId);
                            }
                            threadListBasic.Add(new ThreadBasicInfo
                            {
                                ThreadID = thread.ThreadId,
                                ThreadName = thread.Title,
                                Creator = creatorThread.UserName,
                                CreatedDate = thread.CreatedDate.ToString("H:mm:ss dd/MM/yy"),
                                Status = thread.Status,
                            });
                        }
                    }


                }

                // Get top newest Thread
                topNewestThread = threadListBasic.OrderByDescending(x => x.CreatedDate).Take(top).ToList();


            }
            catch (Exception)
            {

                //throw;
            }
            return topNewestThread;
        }
        /// <summary>
        /// Get list thread follow Like
        /// </summary>
        /// <returns></returns>
        public List<ThreadBasicInfo> GetTopLikeNumberThread(int top)
        {
            List<ThreadBasicInfo> topLikeThread = new List<ThreadBasicInfo>();

            try
            {
                List<int> threadIdList;
                List<ThreadBasicInfo> listThread = new List<ThreadBasicInfo>();
                using (var db = new Ws_DataContext())
                {
                    threadIdList = db.LikeThreads.Select(x => x.ThreadId).Distinct().ToList();
                }

                foreach (int threadId in threadIdList)
                {
                    ThreadBasicInfo thread = GetFullThreadBasicInformation(threadId);
                    listThread.Add(thread);
                }

                topLikeThread = listThread.OrderByDescending(x => x.Likes).Take(top).ToList();

            }
            catch (Exception)
            {

                //throw;
            }
            return topLikeThread;
        }

        /// <summary>
        /// Get threads that belong to a user using user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ThreadBasicInfo> GetThreadsOfUser(int userId)
        {
            List<ThreadBasicInfo> threadList = new List<ThreadBasicInfo>();
            
            try
            {
                List<int> threadIdList;
                using (var db = new Ws_DataContext())
                {
                    threadIdList = db.Threads.Where(x => x.UserId == userId).Select(x => x.ThreadId).ToList();
                }

                foreach (int threadId in threadIdList)
                {
                    ThreadBasicInfo thread = GetFullThreadBasicInformation(threadId);
                    threadList.Add(thread);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return threadList;
        }

        
        /// <summary>
        /// Get information of thread as a thread basic 
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public ThreadBasicInfo GetFullThreadBasicInformation(int threadId)
        {
            ThreadBasicInfo currentThread= new ThreadBasicInfo();

            try
            {
                Thread thread;
                Ws_User user;
                using (var db = new Ws_DataContext())
                {
                    thread = db.Threads.FirstOrDefault(x => x.ThreadId == threadId);
                }

                using (var db = new Ws_DataContext())
                {
                    user = db.Ws_User.FirstOrDefault(x => x.UserID == thread.UserId);
                }

                currentThread.ThreadID = thread.ThreadId;
                currentThread.UserID = thread.UserId;
                currentThread.Creator = user.UserName;
                currentThread.ThreadName = thread.Title;
                currentThread.ImageUrl = GetAllImageThreadById(thread.ThreadId);
                currentThread.ShortDescription = thread.ShortDescription;
                currentThread.Content = thread.Content;
                currentThread.Likes = CountLikeInThread(thread.ThreadId);
                currentThread.Comments = CountCommentInThread(thread.ThreadId);
                currentThread.Status = thread.Status;
                currentThread.CreatedDate = thread.CreatedDate.ToString("H:mm:ss dd/MM/yy");

            }
            catch (Exception)
            {
                //throw;
            }

            return currentThread;
        }
        /// <summary>
        /// Get All Thread in Thread table
        /// </summary>
        /// <returns>list</returns>
        public List<ThreadBasicInfo> GetAllThread()
        {
            var listThread = new List<ThreadBasicInfo>();
            try
            {
                List<int> userIdThread;
                using (var db = new Ws_DataContext())
                {
                    userIdThread = db.Threads.OrderBy(x => x.ThreadId).Select(x => x.ThreadId).ToList();
                }

                using (var db = new ThreadDAL())
                {
                    foreach (int threadId in userIdThread)
                    {
                        ThreadBasicInfo threadBasic = db.GetFullThreadBasicInformation(threadId);
                        listThread.Add(threadBasic);
                    }
                }

            }
            catch (Exception)
            {
                //throw;
            }

            return listThread;
        }
        /// <summary>
        /// Count Comment in Thread
        /// </summary>
        /// <returns>int</returns>
        public int CountCommentInThread(int ThreadId)
        {
            int countComment = 0;
            try
            {
                
                using (var db = new Ws_DataContext())
                {
                   countComment = db.CommentThreads.Count(x => x.ThreadId == ThreadId && x.Status == true);  
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            return countComment;
        }
        public int CountLikeInCommentThread(int CommentId)
        {
            using (var db = new Ws_DataContext())
            {
                int CountLike = db.LikeCommentThreads.Count(x => x.CommentId == CommentId && x.Status == true);
                return CountLike;
            }

        }
        public bool ChangelikeStateForComment(int commentId, string UserName)
        {
            try
            {
                int CurrenUser = 0;
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
                }
                using (var db = new Ws_DataContext())
                {
                    //Check current like status.
                    LikeCommentThread current = (
                                           from p in db.LikeCommentThreads
                                           where p.CommentId == commentId && p.UserId == CurrenUser
                                           select p
                                           ).SingleOrDefault();
                    if (current != null)
                    {
                        current.Status = !current.Status;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.LikeCommentThreads.Add(new LikeCommentThread() { CommentId = commentId, UserId = CurrenUser, Status = true });
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception) { return false; }
        }
        public bool DeleteComment(int CommentId, string UserName)
        {
            using (var db = new Ws_DataContext())
            {
                var currentComment = (from p in db.CommentThreads
                                      where p.CommentThreadId == CommentId && p.Ws_User.UserName.Equals(UserName)
                                      select p).FirstOrDefault();
                if (currentComment != null)
                {
                    var subCommentInComment = (from p in db.SubCommentThread
                                               where p.CommentThreadId == currentComment.CommentThreadId
                                               select p).ToList();
                    if (subCommentInComment != null)
                    {
                        foreach (var item in subCommentInComment)
                        {
                            //Delete All SubComment First
                            db.SubCommentThread.Remove(item);
                        }
                    }
                    var LikeInComment = (from p in db.LikeCommentThreads
                                         where p.CommentId == CommentId
                                         select p).ToList();
                    if (LikeInComment != null)
                    {
                        foreach (var item in LikeInComment)
                        {
                            //Delete All SubComment First
                            db.LikeCommentThreads.Remove(item);
                        }
                    }

                    db.CommentThreads.Remove(currentComment);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public bool DeleteSubComment(int SubCommentId, string UserName)
        {
            using (var db = new Ws_DataContext())
            {
                var currentComment = (from p in db.SubCommentThread
                                      where p.SubCommentThreadId == SubCommentId && p.Ws_User.UserName.Equals(UserName)
                                      select p).FirstOrDefault();
                if (currentComment != null)
                {
                    db.SubCommentThread.Remove(currentComment);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public void Dispose()
        {
            
        }
    }
}