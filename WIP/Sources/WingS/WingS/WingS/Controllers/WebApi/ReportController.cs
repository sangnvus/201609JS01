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
        [HttpPost]
        public IHttpActionResult ReportUser(ReportBasicInfoDTO newReport)
        {
            int currentUser = 0;
            using (var db = new UserDAL())
            {
                currentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            //Set new Report 
            Report r = new Report();
            r.Reason = newReport.Content;
            r.ReportTime = DateTime.Now;
            r.UserId = currentUser;
            r.Type = WsConstant.ReportType.REPORT_USER;
            r.Reason = newReport.Content;
            r.ReportTo = newReport.ReportTo;
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
