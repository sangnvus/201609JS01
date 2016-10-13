using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult SearchResult(string searchKey, string searchType)
        {            
            try
            {
                if (searchType == "thread")
                {
                    var resultThread = new List<ThreadBasicInfo>();
                    using (var db = new SearchDAL())
                    {
                        List<Thread> searchThread = db.SearchThreads(searchKey);
                        if (searchThread.Count < 1)
                        {
                            return Json(new { status = "empty" }, JsonRequestBehavior.AllowGet);
                        }
                        foreach (Thread thread in searchThread)
                        {
                            List<String> threadImage = db.GetAllImageThreadById(thread.ThreadId);
                            resultThread.Add(new ThreadBasicInfo
                            {
                                ThreadID = thread.ThreadId,
                                UserID = thread.UserId,
                                ThreadName = thread.Title,
                                ImageUrl = threadImage,
                                Content = thread.Content,
                                Status = thread.Status,
                                CreatedDate = thread.CreatedDate.ToString("H:mm:ss MM/dd/yy")
                            });
                        }
                    }
                    return Json(new { data = resultThread, type = searchType}, JsonRequestBehavior.AllowGet);
                }
                else if (searchType == "user")
                {
                    var resultUser = new List<SearchUser>();
                    using (var db = new SearchDAL())
                    {
                        List<User_Information> searchUser = db.SearchUsers(searchKey);
                        if (searchUser.Count < 1)
                        {
                            return Json(new { status = "empty" }, JsonRequestBehavior.AllowGet);
                        }
                        foreach (User_Information user in searchUser)
                        {
                            resultUser.Add(new SearchUser
                            {
                                UserID = user.UserID,
                                FullName = user.FullName,
                                ProfileImage = user.ProfileImage,
                                UserAddress = user.UserAddress,
                                Phone = user.Phone,
                                FacebookUrl = user.FacebookUrl
                            });
                        }
                    }
                    return Json(new { data = resultUser, type = searchType}, JsonRequestBehavior.AllowGet);
                }
                else if (searchType == "event")
                {
                    var resultEvent = new List<EventBasicInfo>();
                    using (var db = new SearchDAL())
                    {
                        List<Event> searchEvent = db.SearchEvent(searchKey);
                        if (searchEvent.Count < 1)
                        {
                            return Json(new { status = "empty" }, JsonRequestBehavior.AllowGet);
                        }
                        foreach (Event events in searchEvent)
                        {
                            var eventMainImage = db.GetMainImageEventById(events.EventID);
                            resultEvent.Add(new EventBasicInfo
                            {
                                EventID = events.EventID,
                                CreatorID = events.CreatorID,
                                EventName = events.EventName,
                                ImageUrl = eventMainImage.ImageUrl,
                                Content = events.Description,
                                Status = events.Status,
                                CreatedDate = DateTime.Now.ToString("H:mm:ss MM/dd/yy")
                            });
                        }
                    }
                    return Json(new { data = resultEvent, type = searchType }, JsonRequestBehavior.AllowGet);
                }
                else if (searchType == "organization")
                {
                    var resultOrgz = new List<OrganizationBasicInfo>();
                    using (var db = new SearchDAL())
                    {
                        List<Organazation> getOrg = db.SearchOrganazations(searchKey);
                        if (getOrg.Count < 1)
                        {
                            return Json(new { status = "empty" }, JsonRequestBehavior.AllowGet);
                        }
                        foreach (Organazation org in getOrg)
                        {
                            resultOrgz.Add(new OrganizationBasicInfo
                            {
                                OrganazationID = org.OrganazationID,
                                OrganazationName = org.OrganazationName,
                                Introduction = org.Introduction,
                                LogoUrl = org.LogoUrl,
                                Phone = org.Phone,
                                Email = org.Email,
                                Address = org.Address,
                                Status = org.Status,
                                Point = org.Point
                            });
                        }
                    }
                    return Json(new { data = resultOrgz, type = searchType }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
        }
	}
}   