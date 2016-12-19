using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class AdminOrganizationController : ApiController
    {
        /// <summary>
        /// Get statistic about organization
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStatisticAboutOrganization()
        {
            try
            {
                StatisticManageBasicInforDTO statistic = new StatisticManageBasicInforDTO();

                using (var db = new OrganizationDAL())
                {
                    statistic = db.GetStatisticAboutOrgaization();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = statistic
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }

        [HttpGet]
        public IHttpActionResult GetNewesCreatedOrganization()
        {
            try
            {
                List<OrganizationBasicInfo> orgList = new List<OrganizationBasicInfo>();
                using (var db = new OrganizationDAL())
                {
                    orgList = db.GetNewestCreatedOrgzation();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = orgList
                });
            }
            catch (Exception)
            {

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.ERROR,
                    Message = "",
                    Type = ""
                });
            }
        }
        /// <summary>
        /// Change status of OrganizationId
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ChangeStatusOrganization")]
        public IHttpActionResult ChangeStatusOrganization(int organizationId)
        {
            try
            {
                bool statusOr;
                using (var OrDal = new OrganizationDAL())
                {
                    var organ = OrDal.GetOrganizationById(organizationId);
                    organ.IsActive = !organ.IsActive;
                    statusOr = organ.IsActive;
                    OrDal.UpdateOrganization(organ);
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = statusOr });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }


        /// <summary>
        /// Accept an organization ( make it active )
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult AcceptingOrganization(int organizationId)
        {
            try
            {
                OrganizationBasicInfo organizationBasic;

                using (var db = new OrganizationDAL())
                {
                    var org = db.GetOrganizationById(organizationId);
                    org.IsVerify = true;
                    org.IsActive = true;

                    db.UpdateOrganization(org);

                    organizationBasic = db.GetFullOrganizationBasicInformation(org.OrganizationId);
                }

                
                //send email to register user
                //khai bao bien
                var fromAddress = new MailAddress(WsConstant.OrganizationRegistration.AdminEmail, WsConstant.OrganizationRegistration.WsAdmin);
                var toAddress = new MailAddress(organizationBasic.Creator.Email, organizationBasic.Creator.UserName);
                string fromPassword = WsConstant.OrganizationRegistration.AdminEmailPass;
                string subject = WsConstant.OrganizationRegistration.EmailSubjectAcceptRegistration;
                string body = WsConstant.OrganizationRegistration.EmailContentFirst + "Yêu cầu tạo tổ chức : '" + organizationBasic.OrganizationName + "' " + WsConstant.OrganizationRegistration.EmailContentAcceptRegistration;
                //xu li gui mail
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 30000,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                    
               


                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = true });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }

        /// <summary>
        /// cancel create organization request
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CancelCreateOrganizationRequest(int organizationId)
        {
            try
            {
                bool isSuccess;
                OrganizationBasicInfo organizationBasic;
                // Delete create request
                using (var db = new OrganizationDAL())
                {
                    //get inforamation of organziation Creator
                    organizationBasic = db.GetFullOrganizationBasicInformation(organizationId);

                    isSuccess = db.DeleteOrganization(organizationId);
                }

                
                
                //Send mail to Creator to anounce that admin reject create organization request
                if (isSuccess)
                {
                    //khai bao bien
                    var fromAddress = new MailAddress(WsConstant.OrganizationRegistration.AdminEmail, WsConstant.OrganizationRegistration.WsAdmin);
                    var toAddress = new MailAddress(organizationBasic.Creator.Email, organizationBasic.Creator.UserName);
                    string fromPassword = WsConstant.OrganizationRegistration.AdminEmailPass;
                    string subject = WsConstant.OrganizationRegistration.EmailSubjectRejectRegistration;
                    string body = WsConstant.OrganizationRegistration.EmailContentFirst + "Yêu cầu tạo tổ chức : '" + organizationBasic.OrganizationName + "' " + WsConstant.OrganizationRegistration.EmailContentRejectRegistration;
                    //xu li gui mail
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Timeout = 30000,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = isSuccess });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
    }
}
