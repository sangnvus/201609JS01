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
    public class OrganizationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetRankOfOrganization(int orgId)
        {
            try
            {
                RankingDTO rankOfOrganization;
                using (var db = new OrganizationDAL())
                {
                    Organization org = db.GetOrganizationById(orgId);

                    WsRanking ranking = new WsRanking();
                    rankOfOrganization = ranking.RankingWithPoint(org.Point);
                }



                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = rankOfOrganization
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
        public IHttpActionResult GetOrganizationUsingId(int orgId)
        {
            OrganizationBasicInfo currentOrganization = new OrganizationBasicInfo();
            try
            {
                using (var db = new OrganizationDAL())
                {
                    currentOrganization = db.GetFullOrganizationBasicInformation(orgId);
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = currentOrganization
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
        /// Get all organization order by point
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetOrganizationSortByPoint()
        {
            try
            {
                List<OrganizationBasicInfo> allRankedOrganizaation;

                using (var db = new OrganizationDAL())
                {
                    allRankedOrganizaation = db.GetOrganizationOrderByDecendingPoint();
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = allRankedOrganizaation
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
        public IHttpActionResult GetTopThreeOrganization()
        {
            var basicOrgList = new List<OrganizationBasicInfo>();
            using (var db = new OrganizationDAL())
            {
                List<Organization> topOrg = db.GetTopThreeOrganizations(3);
                foreach (Organization org in topOrg)
                {
                    basicOrgList.Add(new OrganizationBasicInfo
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

                return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.SUCCESS, Data = basicOrgList});
            }
        }

        /// <summary>
        /// Get top organization
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTopOrganization(int top)
        {
            try
            {
                List<OrganizationBasicInfo> orgList;

                using (var db = new OrganizationDAL())
                {
                    orgList = db.GetTopOrganization(top);
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
        /// Get all organization
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllOrganization()
        {
            try
            {
                List<OrganizationBasicInfo> orgList;

                using (var db = new OrganizationDAL())
                {
                    orgList = db.GetAllOrganization();
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
    }
}
