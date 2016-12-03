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
    public class ReportController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetReportContentForUser()
        {
            List<string> reportContent = new List<string>();
            reportContent.Add(WsConstant.ReportUser.BAD_CONTENT);
            reportContent.Add(WsConstant.ReportUser.BAD_POST);
            reportContent.Add(WsConstant.ReportUser.BAD_RULE);
            reportContent.Add(WsConstant.ReportUser.OTHER);
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data= reportContent });
        }
        [HttpGet]
        public IHttpActionResult GetReportContentForOrg()
        {
            List<string> reportContent = new List<string>();
            reportContent.Add(WsConstant.ReportOrg.BAD_CONTENT);
            reportContent.Add(WsConstant.ReportOrg.BAD_EVENT);
            reportContent.Add(WsConstant.ReportOrg.BAD_RULE);
            reportContent.Add(WsConstant.ReportOrg.BAD_ACTION);
            reportContent.Add(WsConstant.ReportUser.OTHER);
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = reportContent });
        }
        [HttpGet]
        public IHttpActionResult GetReportContentForEvent()
        {
            List<string> reportContent = new List<string>();
            reportContent.Add(WsConstant.ReportEvent.BAD_CONTENT);
            reportContent.Add(WsConstant.ReportEvent.BAD_EVENT);
            reportContent.Add(WsConstant.ReportEvent.BAD_RULE);
            reportContent.Add(WsConstant.ReportEvent.BAD_ACTION);
            reportContent.Add(WsConstant.ReportEvent.OTHER);
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = reportContent });
        }
        [HttpGet]
        public IHttpActionResult GetReportContentForThread()
        {
            List<string> reportContent = new List<string>();
            reportContent.Add(WsConstant.ReportThread.BAD_CONTENT);
            reportContent.Add(WsConstant.ReportThread.BAD_THREAD);
            reportContent.Add(WsConstant.ReportThread.BAD_RULE);
            reportContent.Add(WsConstant.ReportThread.BAD_ACTION);
            reportContent.Add(WsConstant.ReportThread.OTHER);
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = reportContent });
        }
        [HttpGet]
        public IHttpActionResult CheckCurrentUserReportedOrNot(string Type,string ReportTo)
        {
            int ReportToId = 0;
            try
            {
                ReportToId = int.Parse(ReportTo);
            }
            catch(Exception)
            {
                using (var db = new Ws_DataContext())
                {
                    ReportToId = db.Ws_User.Where(x => x.UserName == ReportTo).SingleOrDefault().UserID;
                }
            }
         
            using (var db = new ReportDAL())
            {
                bool result = db.CheckCurrentUserReportedOrNot(Type, ReportToId, User.Identity.Name);
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = result });
            }
         
        }
        [HttpGet]
        public IHttpActionResult ReportUser(string Content, string userName)
        {
            int currentUser = 0;
            int toUserId = 0;
            using (var db = new UserDAL())
            {
                currentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
                toUserId = db.GetUserByUserNameOrEmail(userName).UserID;
            }
            //Set new Report 
            Report r = new Report();
            r.Reason = Content;
            r.ReportTime = DateTime.Now;
            r.UserId = currentUser;
            r.Type = WsConstant.ReportType.REPORT_USER;
            r.ReportTo = toUserId;
            r.Status = false;
            r.UpdatedTime = DateTime.Now;
            //Call to accesslayer
            using (var db = new ReportDAL())
            {
                try
                {
                    db.AddNewReport(r);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
                catch(Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }
        }
        [HttpGet]
        public IHttpActionResult ReportOrg(string Content, int toOrgId)
        {
            int currentUser = 0;
            using (var db = new UserDAL())
            {
                currentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            //Set new Report 
            Report r = new Report();
            r.Reason = Content;
            r.ReportTime = DateTime.Now;
            r.UserId = currentUser;
            r.Type = WsConstant.ReportType.REPORT_ORGANAZATION;
            r.ReportTo = toOrgId;
            r.Status = false;
            r.UpdatedTime = DateTime.Now;
            //Call to accesslayer
            using (var db = new ReportDAL())
            {
                try
                {
                    db.AddNewReport(r);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
                catch (Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }
        }
        [HttpGet]
        public IHttpActionResult ReportEvent(string Content, int toEventId)
        {
            int currentUser = 0;
            using (var db = new UserDAL())
            {
                currentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            //Set new Report 
            Report r = new Report();
            r.Reason = Content;
            r.ReportTime = DateTime.Now;
            r.UserId = currentUser;
            r.Type = WsConstant.ReportType.REPORT_EVENT;
            r.ReportTo = toEventId;
            r.Status = false;
            r.UpdatedTime = DateTime.Now;
            //Call to accesslayer
            using (var db = new ReportDAL())
            {
                try
                {
                    db.AddNewReport(r);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
                catch (Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }
        }
        [HttpGet]
        public IHttpActionResult ReportThread(string Content, int toThreadId)
        {
            int currentUser = 0;
            using (var db = new UserDAL())
            {
                currentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            //Set new Report 
            Report r = new Report();
            r.Reason = Content;
            r.ReportTime = DateTime.Now;
            r.UserId = currentUser;
            r.Type = WsConstant.ReportType.REPORT_THREAD;
            r.ReportTo = toThreadId;
            r.Status = false;
            r.UpdatedTime = DateTime.Now;
            //Call to accesslayer
            using (var db = new ReportDAL())
            {
                try
                {
                    db.AddNewReport(r);
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
                catch (Exception)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
                }
            }
        }

        /// <summary>
        /// Get report statistic about user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserReportData()
        {
            try
            {
                List<ReportStatisticDTO> reportStatistic = new List<ReportStatisticDTO>();

                using (var reportDal = new ReportDAL())
                {
                    reportStatistic = reportDal.GetListReportByType(WsConstant.ReportType.REPORT_USER);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get User Report Data Successfully",
                    Type = "",
                    Data = reportStatistic
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot User Report Data Profile!",
                    Type = ""
                });
            }
        }

        /// <summary>
        /// Get user report detail data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserReportDetailData(int userId)
        {
            try
            {
                List<ReportBasicInfoDTO> userDetailReportData = new List<ReportBasicInfoDTO>();

                using (var reportDal = new ReportDAL())
                {
                    userDetailReportData = reportDal.GetReportDetailData(WsConstant.ReportType.REPORT_USER);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "Get User Detail Report  Data Successfully",
                    Type = "",
                    Data = userDetailReportData
                });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "Cannot User Detail Report Data Profile!",
                    Type = ""
                });
            }
        }
    }
}
