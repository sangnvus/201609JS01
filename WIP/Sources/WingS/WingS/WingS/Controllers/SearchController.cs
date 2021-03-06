﻿using System;
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
                searchKey = searchKey.Trim();
                if (searchKey != "")
                {

                    if (searchType == "thread")
                    {
                        var resultThread = new List<ThreadBasicInfo>();
                        using (var db = new SearchDAL())
                        {
                            List<Thread> searchThread = db.SearchThreads(searchKey);
                            if (searchThread.Count < 1)
                            {
                                return Json(new {status = "empty"}, JsonRequestBehavior.AllowGet);
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
                                    ShortDescription = thread.ShortDescription,
                                    Status = thread.Status,
                                    CreatedDate = thread.CreatedDate.ToString("H:mm:ss MM/dd/yy")
                                });
                            }
                        }
                        return Json(new {data = resultThread, type = searchType}, JsonRequestBehavior.AllowGet);
                    }
                    else if (searchType == "user")
                    {
                        var resultUser = new List<SearchUser>();
                        using (var db = new SearchDAL())
                        {
                            List<Ws_User> searchUser = db.SearchUsers(searchKey);
                            if (searchUser.Count < 1)
                            {
                                return Json(new {status = "empty"}, JsonRequestBehavior.AllowGet);
                            }
                            foreach (Ws_User user in searchUser)
                            {
                                resultUser.Add(db.GetUserInfoById(user.UserID, user.UserName));
                            }
                        }
                        return Json(new {data = resultUser, type = searchType}, JsonRequestBehavior.AllowGet);
                    }
                    else if (searchType == "userinfo")
                    {
                        var resultUser = new List<SearchUser>();
                        using (var db = new SearchDAL())
                        {
                            List<User_Information> searchUser = db.SearchUserInfo(searchKey);
                            if (searchUser.Count < 1)
                            {
                                return Json(new {status = "empty"}, JsonRequestBehavior.AllowGet);
                            }
                            foreach (User_Information user in searchUser)
                            {
                                resultUser.Add(new SearchUser
                                {
                                    UserID = user.UserID,
                                    UserName = db.GetUserNameById(user.UserID),
                                    FullName = user.FullName,
                                    ProfileImage = user.ProfileImage,
                                    UserAddress = user.UserAddress,
                                    Phone = user.Phone,
                                    FacebookUrl = user.FacebookUrl
                                });
                            }
                        }
                        return Json(new {data = resultUser, type = searchType}, JsonRequestBehavior.AllowGet);
                    }
                    else if (searchType == "event")
                    {
                        var resultEvent = new List<EventBasicInfo>();
                        using (var db = new SearchDAL())
                        {
                            List<Event> searchEvent = db.SearchEvent(searchKey);
                            if (searchEvent.Count < 1)
                            {
                                return Json(new {status = "empty"}, JsonRequestBehavior.AllowGet);
                            }
                            foreach (Event events in searchEvent)
                            {
                                var eventMainImage = db.GetMainImageEventById(events.EventID);
                                resultEvent.Add(new EventBasicInfo
                                {
                                    EventID = events.EventID,
                                    CreatorID = events.CreatorID,
                                    EventName = events.EventName,
                                    MainImageUrl = eventMainImage.ImageUrl,
                                    ShortDescription = events.ShortDescription,
                                    Status = events.Status,
                                    CreatedDate = DateTime.Now.ToString("H:mm:ss MM/dd/yy")
                                });
                            }
                        }
                        return Json(new {data = resultEvent, type = searchType}, JsonRequestBehavior.AllowGet);
                    }
                    else if (searchType == "organization")
                    {
                        var resultOrgz = new List<OrganizationBasicInfo>();
                        using (var db = new SearchDAL())
                        {
                            List<Organization> getOrg = db.SearchOrganizations(searchKey);
                            if (getOrg.Count < 1)
                            {
                                return Json(new {status = "empty"}, JsonRequestBehavior.AllowGet);
                            }
                            foreach (Organization org in getOrg)
                            {
                                resultOrgz.Add(new OrganizationBasicInfo
                                {
                                    OrganizationId = org.OrganizationId,
                                    OrganizationName = org.OrganizationName,
                                    Introduction = org.Introduction,
                                    LogoUrl = org.LogoUrl,
                                    Phone = org.Phone,
                                    Email = org.Email,
                                    Address = org.Address,
                                    IsActive = org.IsActive,
                                    Point = org.Point
                                });
                            }
                        }
                        return Json(new {data = resultOrgz, type = searchType}, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new {status = "error"}, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = "emptyinput" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }
        }
	}
}   