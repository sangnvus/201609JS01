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
