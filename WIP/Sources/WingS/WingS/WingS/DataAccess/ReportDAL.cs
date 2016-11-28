using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class ReportDAL : IDisposable
    {
        public Report AddNewReport(Report newReport)
        {
            using (var db = new Ws_DataContext())
            {
                var currentReport = db.Reports.Add(newReport);
                db.SaveChanges();
                return currentReport;
            }
        }
        public bool CheckCurrentUserReportedOrNot(string Type,int ReportTo, string UserName)
        {

            using (var db = new Ws_DataContext())
            {
               int CurrentUserId = db.Ws_User.Where(x => x.UserName == UserName).FirstOrDefault().UserID;
                var check = db.Reports.Where(x => x.UserId == CurrentUserId && x.ReportTo == ReportTo && x.Type == Type).SingleOrDefault();
                if (check != null) return true;
                else return false;
            }
        }
        public void Dispose()
        {
            
        }
    }
}