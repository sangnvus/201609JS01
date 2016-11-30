using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        public List<OrganizationBasicInfo> GetOrganizationOrderByDecendingPoint()
        {
            List<OrganizationBasicInfo> orgList = new List<OrganizationBasicInfo>();

            try
            {
                List<int> orgIdList;
                //Take all organization id and order decending by  point
                using (var db = new Ws_DataContext())
                {
                    orgIdList = db.Organizations.OrderByDescending(x => x.Point).Select(x => x.OrganizationId).ToList();
                }

                //Get information for each organization id
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
            newOrg.IsVerify = false;
            newOrg.IsActive = false;
            newOrg.Point = 0;
            newOrg.CreatedDate = DateTime.Now;

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
                    organizationBasic.CreatedDate = org.CreatedDate.ToString("H:mm:ss dd/MM/yy");
                    organizationBasic.Point = org.Point;
                    using (var db = new Ws_DataContext())
                    {
                        organizationBasic.CreatorName = db.Organizations.Where(x => x.OrganizationId == orgId).SingleOrDefault().Ws_User.UserName;
                        organizationBasic.NumberOfEvent = db.Events.Where(x => x.CreatorID == organizationBasic.OrganizationId).Count();
                    }

                        WsRanking ranking = new WsRanking();
                    RankingDTO rank = ranking.RankingWithPoint(org.Point);
                    if (rank.CurrentRank == 0)
                    {
                        organizationBasic.CurrentRank = "Mới";
                    }
                    else if (rank.CurrentRank == 200)
                    {
                        organizationBasic.CurrentRank = "Đồng";
                    }
                    else if (rank.CurrentRank == 500)
                    {
                        organizationBasic.CurrentRank = "Bạc";
                    }
                    else if (rank.CurrentRank == 2000)
                    {
                        organizationBasic.CurrentRank = "Vàng";
                    }
                    else if (rank.CurrentRank == 5000)
                    {
                        organizationBasic.CurrentRank = "Bạch Kim";
                    }
                    else if (rank.CurrentRank == 10000)
                    {
                        organizationBasic.CurrentRank = "Kim Cương";
                    }
                    //get creator
                    using (var db = new UserDAL())
                    {
                        organizationBasic.Creator = db.GetFullInforOfUserAsBasicUser(orgId);
                    }
                }

                
            }
            catch (Exception)
            {
                
                //throw;
            }

            return organizationBasic;
        }

        public StatisticManageBasicInforDTO GetStatisticAboutOrgaization()
        {
            StatisticManageBasicInforDTO statistic = new StatisticManageBasicInforDTO();

            try
            {
                statistic.NumberActiveOrganization = CountOrganizationActiveOrNot(true);
                statistic.NumberNotActiveOrganization = CountOrganizationActiveOrNot(false);
                statistic.NumberNotVerifyOrganization = CountOrganizationVerifyOrNot(false);
                statistic.NumberTotalOrganization = CountTotalOrganization();
            }
            catch (Exception)
            {
                //throw;
            }

            return statistic;
        }

        /// <summary>
        /// Count total Organization in database
        /// </summary>
        /// <returns>int</returns>
        public int CountTotalOrganization()
        {
            int numberUser = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberUser = db.Organizations.Count();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberUser;
        }

        /// <summary>
        /// Count number of Organization is verified or not
        /// </summary>
        /// <param name="isVerify"></param>
        /// <returns></returns>
        public int CountOrganizationVerifyOrNot(bool isVerify)
        {
            int numberOrg = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    
                    numberOrg = isVerify ? db.Organizations.Count(x => x.IsVerify) : db.Organizations.Count(x => x.IsVerify == false);
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return numberOrg;
        }

        /// <summary>
        /// Count number of Organization is actived or not
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public int CountOrganizationActiveOrNot(bool isActive)
        {
            int numberOrg = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    numberOrg = isActive ? db.Organizations.Count(x => x.IsVerify == true && x.IsActive == true) : db.Organizations.Count(x => x.IsVerify == true && x.IsActive == false);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return numberOrg;
        }

        /// <summary>
        /// Get organization have been create less than 30 day
        /// </summary>
        /// <returns></returns>
        public List<OrganizationBasicInfo> GetNewestCreatedOrgzation()
        {
            List<OrganizationBasicInfo> newestOrg = new List<OrganizationBasicInfo>();
            try
            {
                List<int> orgIdList;
                using (var db = new Ws_DataContext())
                {
                    DateTime dateBeforeThrityDay = DateTime.UtcNow.AddDays(-30);
                    orgIdList = db.Organizations.OrderByDescending(x => x.CreatedDate).Where(x => x.CreatedDate >= dateBeforeThrityDay).Select(x=>x.OrganizationId).ToList();
                    foreach (int orgId in orgIdList)
                    {
                        OrganizationBasicInfo organization = GetFullOrganizationBasicInformation(orgId);
                        newestOrg.Add(organization);
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            return newestOrg;
        }

        /// <summary>
        /// Get all organization
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<OrganizationBasicInfo> GetAllOrganization()
        {
            List<OrganizationBasicInfo> orgList = new List<OrganizationBasicInfo>();

            try
            {
                List<int> orgIdList;
                using (var db = new Ws_DataContext())
                {
                    orgIdList = db.Organizations.Select(x => x.OrganizationId).ToList();
                }

                orgList.AddRange(orgIdList.Select(GetFullOrganizationBasicInformation));
            }
            catch (Exception)
            {

                //throw;
            }

            return orgList;
        }
        // Update Organization
        public Organization UpdateOrganization(Organization organ)
        {
            try
            {
                using (var db = new Ws_DataContext())
                {
                    db.Organizations.AddOrUpdate(organ);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return organ;
        }

        /// <summary>
        /// get all organization wait for accept
        /// </summary>
        /// <returns></returns>
        public List<OrganizationBasicInfo> GetAllOrganizationWaitForAcception()
        {
            List<OrganizationBasicInfo> orgList = new List<OrganizationBasicInfo>();

            try
            {
                List<int> orgIdList;
                using (var db = new Ws_DataContext())
                {
                    orgIdList = db.Organizations.Where(x=>x.IsVerify==false).Select(x => x.OrganizationId).ToList();
                }

                orgList.AddRange(orgIdList.Select(GetFullOrganizationBasicInformation));
            }
            catch (Exception)
            {

                //throw;
            }

            return orgList;
        }

        /// <summary>
        /// Delete orgaization with id
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool DeleteOrganization(int orgId)
        {
            try
            {
                using (var db = new Ws_DataContext())
                {
                    Organization organization = db.Organizations.FirstOrDefault(x => x.OrganizationId == orgId);
                        
                    if (organization != null)
                    {
                        db.Organizations.Remove(organization);
                        db.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            
        }

        public void Dispose()
        {
           
        }
    }
}