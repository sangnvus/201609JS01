using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            return PartialView("~/Views/Admin/_DashBoard.cshtml");
        }

        public ActionResult AdminLogin()
        {
            return PartialView("~/Views/Admin/_AdminLogin.cshtml");
        }
        public ActionResult UserDashBoard()
        {
            return PartialView("~/Views/Admin/UserManage/_UserDashBoard.cshtml");
        }
        public ActionResult UserList()
        {
            return PartialView("~/Views/Admin/UserManage/_UserList.cshtml");
        }
        public ActionResult UserProfile()
        {
            return PartialView("~/Views/Admin/UserManage/_UserProfile.cshtml");
        }
        public ActionResult ThreadDashBoard()
        {
            return PartialView("~/Views/Admin/ThreadManage/_ThreadDashBoard.cshtml");
        }
        public ActionResult ThreadList()
        {
            return PartialView("~/Views/Admin/ThreadManage/_ThreadList.cshtml");
        }
        public ActionResult ThreadDetail()
        {
            return PartialView("~/Views/Admin/ThreadManage/_ThreadDetail.cshtml");
        }
        public ActionResult EventDashBoard()
        {
            return PartialView("~/Views/Admin/EventManage/_EventDashBoard.cshtml");
        }
        public ActionResult EventList()
        {
            return PartialView("~/Views/Admin/EventManage/_EventList.cshtml");
        }
        public ActionResult EventType()
        {
            return PartialView("~/Views/Admin/EventManage/_EventType.cshtml");
        }
        public ActionResult EventDetail()
        {
            return PartialView("~/Views/Admin/EventManage/_EventDetail.cshtml");
        }
        public ActionResult OrganizationDashBoard()
        {
            return PartialView("~/Views/Admin/OrganizationManage/_OrganizationDashBoard.cshtml");
        }
        public ActionResult OrganizationList()
        {
            return PartialView("~/Views/Admin/OrganizationManage/_OrganizationList.cshtml");
        }
        public ActionResult OrganizationDetail()
        {
            return PartialView("~/Views/Admin/OrganizationManage/_OrganizationDetail.cshtml");
        }
        public ActionResult DonationInfo()
        {
            return PartialView("~/Views/Admin/_Donationinfo.cshtml");
        }
        public ActionResult ReportUser()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportUser.cshtml");
        }
        public ActionResult ReportUserDetail()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportUserDetail.cshtml");
        }
        public ActionResult ReportOrganization()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportOrganization.cshtml");
        }
        public ActionResult ReportOrganizationDetail()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportOrganizationDetail.cshtml");
        }
        public ActionResult ReportEvent()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportEvent.cshtml");
        }
        public ActionResult ReportThread()
        {
            return PartialView("~/Views/Admin/ReportManage/_ReportThread.cshtml");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}