﻿using System;
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
        public IHttpActionResult GetOrganizationBeLongToUser(int orgId)
        {
            OrganizationBasicInfo orgBasicInfo = new OrganizationBasicInfo();
            try
            {
                using (var db = new OrganizationDAL())
                {
                    Organization org = db.GetOrganizationById(orgId);
                    orgBasicInfo.OrganizationId = org.OrganizationId;
                    orgBasicInfo.OrganizationName = org.OrganizationName;
                    orgBasicInfo.Introduction = org.Introduction;
                    orgBasicInfo.LogoUrl = org.LogoUrl;
                    orgBasicInfo.Phone = org.Phone;
                    orgBasicInfo.Email = org.Email;
                    orgBasicInfo.Address = org.Address;
                    orgBasicInfo.Status = org.Status;
                    orgBasicInfo.Point = org.Point;
                }

                return Ok(new HTTPMessageDTO
                {
                    Status = WsConstant.HttpMessageType.SUCCESS,
                    Message = "",
                    Type = "",
                    Data = orgBasicInfo
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
        public IHttpActionResult GetOrganizationSortByPoint()
        {
            var orgBasicInforList = new List<OrganizationBasicInfo>();
            List<Organization> orgSortedPointList;
            using (var db = new OrganizationDAL())
            {
                orgSortedPointList = db.GetOrganizationWithSortedPoint();
                foreach (Organization org in orgSortedPointList)
                {
                    orgBasicInforList.Add(new OrganizationBasicInfo
                    {
                        OrganizationId = org.OrganizationId,
                        OrganizationName = org.OrganizationName,
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

            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = orgBasicInforList });
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
                        Status = org.Status,
                        Point = org.Point
                    });
                }

                return Ok(new HTTPMessageDTO {Status = WsConstant.HttpMessageType.SUCCESS, Data = basicOrgList});
            }
        }
    }
}
