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
                var allSortedOrg = db.Organizations.OrderByDescending(x => x.Point).Where(x => x.IsActive == true);
                listOrg = allSortedOrg.ToList();
            }

            return listOrg;
        } 

        public Organization AddNewOrganization(CreateOrganization organizationBasic, string UserName)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
            }
            var newOrg = CreatEmptyOrganization();
            newOrg.OrganizationId = CurrenUser;
            newOrg.OrganizationName = organizationBasic.OrganizationName;
            newOrg.EOrganizationName = ConvertToUnSign.Convert(organizationBasic.OrganizationName);
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

        public Boolean EditOrganization(CreateOrganization organizationBasic, string UserName)
        {
            try
            {
                int CurrenUser = 0;
                using (var db = new UserDAL())
                {
                    CurrenUser = db.GetUserByUserNameOrEmail(UserName).UserID;
                }
                using (var db = new Ws_DataContext())
                {
                    var currentOrg = db.Organizations.Find(CurrenUser);

                    currentOrg.OrganizationName = organizationBasic.OrganizationName;
                    currentOrg.OrganizationName = organizationBasic.OrganizationName;
                    currentOrg.Introduction = organizationBasic.Introduction;
                    if (organizationBasic.LogoUrl != "" && organizationBasic.LogoUrl != null)
                    {
                        currentOrg.LogoUrl = organizationBasic.LogoUrl;
                    }
                    currentOrg.Phone = organizationBasic.Phone;
                    currentOrg.Email = organizationBasic.Email;
                    currentOrg.Address = organizationBasic.Address;

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            
            
           
        }
        public Organization CreatEmptyOrganization()
        {
            using (var db = new Ws_DataContext())
            {
                var org = db.Organizations.Create();
                
                org.OrganizationName = "";
                org.EOrganizationName = "";
                org.Introduction = "";
                org.LogoUrl = "";
                org.Phone = "";
                org.Email = "";
                org.Address = "";
                org.IsActive = false;
                org.IsVerify = false;
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

        /// <summary>
        /// Get top of organization sorting by point
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<OrganizationBasicInfo> GetTopOrganization(int top)
        {
            List<OrganizationBasicInfo> orgList = new List<OrganizationBasicInfo>();

            try
            {
                List<int> orgIdList;
                using (var db = new Ws_DataContext())
                {
                    orgIdList = db.Organizations.OrderByDescending(x => x.Point).Select(x=>x.OrganizationId).Take(top).ToList();
                }

                foreach (int orgId in orgIdList)
                {
                    OrganizationBasicInfo organization = GetFullOrganizationBasicInformation(orgId);
                    orgList.Add(organization);
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            return orgList;
        } 

        public OrganizationBasicInfo GetFullOrganizationBasicInformation(int orgId)
        {
            OrganizationBasicInfo organizationBasic = new OrganizationBasicInfo();

            try
            {
                Organization org = GetOrganizationById(orgId);
                //get ranking information
                

                organizationBasic.OrganizationId = orgId;
                if (org != null)
                {
                    organizationBasic.OrganizationName = org.OrganizationName;
                    organizationBasic.Introduction = org.Introduction;
                    organizationBasic.LogoUrl = org.LogoUrl;
                    organizationBasic.Phone = org.Phone;
                    organizationBasic.Email = org.Email;
                    organizationBasic.Address = org.Address;
                    organizationBasic.IsActive = org.IsActive;
                    organizationBasic.IsVerify = org.IsVerify;
                    organizationBasic.Point = org.Point;

                    WsRanking ranking = new WsRanking();
                    RankingDTO rank = ranking.RankingWithPoint(org.Point);
                    if (rank.CurrentRank == 0)
                    {
                        organizationBasic.CurrentRank = "New";
                    }
                    else if (rank.CurrentRank == 200)
                    {
                        organizationBasic.CurrentRank = "Bronze";
                    }
                    else if (rank.CurrentRank == 500)
                    {
                        organizationBasic.CurrentRank = "Silver";
                    }
                    else if (rank.CurrentRank == 2000)
                    {
                        organizationBasic.CurrentRank = "Golden";
                    }
                    else if (rank.CurrentRank == 5000)
                    {
                        organizationBasic.CurrentRank = "Plantium";
                    }
                    else if (rank.CurrentRank == 10000)
                    {
                        organizationBasic.CurrentRank = "Diamon";
                    }
                    
                }

                
            }
            catch (Exception)
            {
                
                //throw;
            }

            return organizationBasic;
        }
        public void Dispose()
        {
           
        }
    }
}