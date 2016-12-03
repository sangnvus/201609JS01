using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.DataHelper;
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

        public ReportBasicInfoDTO GetReportBasicInformation(int reportId)
        {
            try
            {
                var reportBasic = new ReportBasicInfoDTO();
                Report report;
                string reportorName = "";
                using (var db = new Ws_DataContext())
                {
                    report = db.Reports.FirstOrDefault(x => x.ReportId == reportId);
                    
                    if (report!=null)
                    {
                        reportBasic.ReportId = report.ReportId;
                        reportBasic.UserId = report.UserId;
                        var userMakeReport = db.Ws_User.FirstOrDefault(x=>x.UserID == reportBasic.UserId);
                        var userInformationMakeReport = db.User_Information.FirstOrDefault(x => x.UserID == reportBasic.UserId);
                        if (userMakeReport != null)
                        {
                            reportBasic.ReportorUserName = userMakeReport.UserName;
                        }

                        if (userInformationMakeReport != null)
                        {
                            reportBasic.ReportorImage = userInformationMakeReport.ProfileImage;
                        }
                        else
                        {
                            reportBasic.ReportorImage = "~/Content/Images/Slider/avatar_default.png";
                        }
                            
                        reportBasic.Reason = report.Reason;
                        reportBasic.ReportTime = report.ReportTime.ToString("HH:mm:ss dd/mm/yyyy");
                        reportBasic.Type = report.Type;
                        reportBasic.ReportTo = report.ReportTo;
                        reportBasic.Status = report.Status;
                        reportBasic.UpdatedTime = report.UpdatedTime.ToString("HH:mm:ss dd/mm/yyyy");

                        if (reportBasic.Type.Equals(WsConstant.ReportType.REPORT_USER))
                        {
                            var userIsReported = db.Ws_User.FirstOrDefault(x => x.UserID == reportBasic.ReportTo);
                            if (userIsReported != null)
                            {
                                reportBasic.ReportToName = userIsReported.UserName;
                            }
                                
                            var userIsReportedInformation = db.User_Information.FirstOrDefault(x => x.UserID == reportBasic.ReportTo);
                            if (userIsReportedInformation != null)
                            {
                                reportBasic.ReportToImage = userIsReportedInformation.ProfileImage;
                            }
                                
                        }
                        else if (reportBasic.Type.Equals(WsConstant.ReportType.REPORT_EVENT))
                        {
                            var eventIsReported = db.Events.FirstOrDefault(x => x.EventID == reportBasic.ReportTo);
                            if (eventIsReported != null)
                            {
                                reportBasic.ReportToName = eventIsReported.EventName;
                            }

                            using (var eventDal = new EventDAL())
                            {
                                reportBasic.ReportToImage = eventDal.GetMainImageEventById(reportBasic.ReportTo).ImageUrl;
                            }
                        }
                        else if (reportBasic.Type.Equals(WsConstant.ReportType.REPORT_THREAD))
                        {
                            var threadIsReported = db.Threads.FirstOrDefault(x => x.ThreadId == reportBasic.ReportTo);
                            if (threadIsReported != null)
                            {
                                reportBasic.ReportToName = threadIsReported.Title;
                            }

                            using (var threadDal = new ThreadDAL())
                            {
                                reportBasic.ReportToImage = threadDal.GetAllImageThreadById(reportBasic.ReportTo).First();
                            }
                                
                        }
                        else if (reportBasic.Type.Equals(WsConstant.ReportType.REPORT_ORGANAZATION))
                        {
                            var orgIsReported = db.Organizations.FirstOrDefault(x => x.OrganizationId == reportBasic.ReportTo);
                            if (orgIsReported != null)
                            {
                                reportBasic.ReportToName = orgIsReported.OrganizationName;
                                reportBasic.ReportToImage = orgIsReported.LogoUrl;
                            }
                        }
                    }
                }
                
                return reportBasic;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        /// <summary>
        /// get report statistic data
        /// </summary>
        /// <param name="typeReport"></param>
        /// <returns></returns>
        public List<ReportStatisticDTO> GetListReportByType(string typeReport)
        {
            List<ReportStatisticDTO> listReport = new List<ReportStatisticDTO>();
            List<int> isReportedIdList;
            try
            {
                var db = new Ws_DataContext();
                var eventDal = new EventDAL();
                var threadDal = new ThreadDAL();

                // lấy ra danh sách id bị report theo type
                isReportedIdList =
                    db.Reports.Where(x => x.Type.Equals(typeReport)).Select(x => x.ReportTo).Distinct().ToList();
                    
                foreach (var id in isReportedIdList)
                {
                    var reportStatistic = new ReportStatisticDTO();
                    reportStatistic.IsReportedId = id;

                    if (typeReport.Equals(WsConstant.ReportType.REPORT_USER))
                    {
                        var userIsReported = db.Ws_User.FirstOrDefault(x => x.UserID == id);
                        if (userIsReported != null)
                        {
                            reportStatistic.IsreportedUserName = userIsReported.UserName;
                        }

                        var userIsReportedInformation = db.User_Information.FirstOrDefault(x => x.UserID == id);
                        if (userIsReportedInformation != null)
                        {
                            reportStatistic.IsreportedImage = userIsReportedInformation.ProfileImage;
                        }
                    }
                    else if (typeReport.Equals(WsConstant.ReportType.REPORT_EVENT))
                    {
                        var eventIsReported = db.Events.FirstOrDefault(x => x.EventID == id);
                        if (eventIsReported != null)
                        {
                            reportStatistic.IsreportedUserName = eventIsReported.EventName;
                        }
                            
                        reportStatistic.IsreportedImage = eventDal.GetMainImageEventById(id).ImageUrl;
                    }
                    else if (typeReport.Equals(WsConstant.ReportType.REPORT_THREAD))
                    {
                        var threadIsReported = db.Threads.FirstOrDefault(x => x.ThreadId == id);
                        if (threadIsReported != null)
                        {
                            reportStatistic.IsreportedUserName = threadIsReported.Title;
                        }

                        reportStatistic.IsreportedImage = threadDal.GetAllImageThreadById(id).First();
                    }
                    else if (typeReport.Equals(WsConstant.ReportType.REPORT_ORGANAZATION))
                    {
                        var orgIsReported = db.Organizations.FirstOrDefault(x => x.OrganizationId == id);
                        if (orgIsReported != null)
                        {
                            reportStatistic.IsreportedUserName = orgIsReported.OrganizationName;
                            reportStatistic.IsreportedImage = orgIsReported.LogoUrl;
                        }
                    }
                        

                    reportStatistic.TotalReportedTimes = CountIsReportedTime(id, typeReport);
                    reportStatistic.NewReportedTimes = CountNewReportedTime(id, typeReport);

                    listReport.Add(reportStatistic);
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return listReport;
        }

        public int CountIsReportedTime(int isReportedId , string reportType)
        {
            var times = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    times = db.Reports.Count(x => x.ReportTo == isReportedId && x.Type == reportType);
                }
            }
            catch (Exception)
            {
                return 0;
                //throw;
            }

            return times;
        }

        public int CountNewReportedTime(int isReportedId, string reportType)
        {
            var times = 0;
            try
            {
                using (var db = new Ws_DataContext())
                {
                    times = db.Reports.Count(x => x.ReportTo == isReportedId && x.Type == reportType && x.Status == true);
                }
            }
            catch (Exception)
            {
                return 0;
                //throw;
            }

            return times;
        }

        public List<ReportBasicInfoDTO> GetReportDetailData(string typeReport)
        {
            List<ReportBasicInfoDTO> reportDetailData = new List<ReportBasicInfoDTO>();
            try
            {
                using (var db = new Ws_DataContext())
                {
                    var reportIdList = db.Reports.Where(x => x.Type == typeReport).Select(x => x.ReportId).ToList();

                    foreach (var reportId in reportIdList)
                    {
                        var reportBasic = GetReportBasicInformation(reportId);
                        reportDetailData.Add(reportBasic);
                    }
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }

            return reportDetailData;
        } 

        public void Dispose()
        {
            
        }
    }
}