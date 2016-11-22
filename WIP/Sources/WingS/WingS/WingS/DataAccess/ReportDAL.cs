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
        public void Dispose()
        {
            
        }
    }
}