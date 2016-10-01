using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;

namespace WingS.DataAccess
{
    public class OrganizationDAL : IDisposable
    {
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