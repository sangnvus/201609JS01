using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class OrganizationDAL : IDisposable
    {
        /// <summary>
        /// Get Organization with sort descending follow point
        /// </summary>
        /// <returns>List of Organization</returns>
        public List<Organization> GetOrganizationWithSortedPoint()
        {
            List<Organization> listOrg;

            using (var db = new Ws_DataContext())
            {
                var allSortedOrg = db.Organizations.OrderByDescending(x => x.Point).Where(x => x.Status == true);
                listOrg = allSortedOrg.ToList();
            }

            return listOrg;
        } 

        public Organization AddNewOrganization(CreateOrganization organizationBasic)
        {
            var newOrg = CreatEmptyOrganization();
            newOrg.OrganizationId = WsConstant.CurrentUser.UserId;
            newOrg.OrganizationName = organizationBasic.OrganizationName;
            newOrg.Introduction = organizationBasic.Introduction;
            newOrg.LogoUrl = organizationBasic.LogoUrl;
            newOrg.Phone = organizationBasic.Phone;
            newOrg.Email = organizationBasic.Email;
            newOrg.Address = organizationBasic.Address;

            using (var db = new Ws_DataContext())
            {
                db.Organizations.Add(newOrg);
                db.SaveChanges();
                return GetOrganizationById(newOrg.OrganizationId);
            }
        }

        public Organization CreatEmptyOrganization()
        {
            using (var db = new Ws_DataContext())
            {
                var org = db.Organizations.Create();
                
                org.OrganizationName = "";
                org.Introduction = "";
                org.LogoUrl = "";
                org.Phone = "";
                org.Email = "";
                org.Address = "";
                org.Status = true;
                org.Point = 0;

                return org;
            }
        }

        public Organization GetOrganizationById(int orgId)
        {
            using (var db = new Ws_DataContext())
            {
                var org = db.Organizations.FirstOrDefault(x => x.OrganizationId == orgId);
                return org;
            }
        }

        /// <summary>
        /// Get top 3 organization sort by Point
        /// </summary>
        /// <param name="numberOrg"></param>
        /// <returns></returns>
        public List<Organization> GetTopThreeOrganizations(int numberOrg)
        {
            List<Organization> orgList = null;
            using (var db = new Ws_DataContext())
            {
                var topOrg = db.Organizations.OrderByDescending(x => x.Point).Take(numberOrg);
                orgList = topOrg.ToList();
            }
            return orgList;
        } 

        public void Dispose()
        {
           
        }
    }
}