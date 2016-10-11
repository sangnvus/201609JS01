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
        public Organazation AddNewOrganazation(CreateOrganization organizationBasic)
        {
            var newOrg = CreatEmptyOrganization();

            newOrg.OrganazationID = WsConstant.CurrentUser.UserId;
            newOrg.OrganazationName = organizationBasic.OrganazationName;
            newOrg.Introduction = organizationBasic.Introduction;
            newOrg.LogoUrl = organizationBasic.LogoUrl;
            newOrg.Phone = organizationBasic.Phone;
            newOrg.Email = organizationBasic.Email;
            newOrg.Address = organizationBasic.Address;

            using (var db = new Ws_DataContext())
            {
                db.Organazations.Add(newOrg);
                db.SaveChanges();
                return GetOrganizationById(newOrg.OrganazationID);
            }
        }

        public Organazation CreatEmptyOrganization()
        {
            using (var db = new Ws_DataContext())
            {
                var org = db.Organazations.Create();
                
                org.OrganazationName = "";
                org.Introduction = "";
                org.LogoUrl = "";
                org.Phone = "";
                org.Email = "";
                org.Address = "";
                org.Status = false;
                org.Point = 0;

                return org;
            }
        }

        public Organazation GetOrganizationById(int orgId)
        {
            using (var db = new Ws_DataContext())
            {
                var org = db.Organazations.FirstOrDefault(x => x.OrganazationID == orgId);
                return org;
            }
        }

        /// <summary>
        /// Get top 3 organization sort by Point
        /// </summary>
        /// <param name="numberOrg"></param>
        /// <returns></returns>
        public List<Organazation> GetTopThreeOrganazations(int numberOrg)
        {
            List<Organazation> orgList = null;
            using (var db = new Ws_DataContext())
            {
                var topOrg = db.Organazations.OrderByDescending(x => x.Point).Take(numberOrg);
                orgList = topOrg.ToList();
            }
            return orgList;
        } 

        public void Dispose()
        {
           
        }
    }
}