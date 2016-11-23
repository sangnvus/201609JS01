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
    }
}
