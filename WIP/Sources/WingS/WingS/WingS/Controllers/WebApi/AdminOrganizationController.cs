using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                using (var db = new OrganizationDAL())
                {
                    var org = db.GetOrganizationById(organizationId);
                    org.IsVerify = true;
                    org.IsActive = true;

                    db.UpdateOrganization(org);
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
                // Delete create request
                using (var db = new OrganizationDAL())
                {
                    isSuccess = db.DeleteOrganization(organizationId);
                }

                //Send mail to Creator

                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = isSuccess });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
    }
}
