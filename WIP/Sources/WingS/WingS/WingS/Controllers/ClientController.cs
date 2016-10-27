using System.Web.Mvc;
using WingS.DataAccess;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class ClientController : Controller
    {
        public ActionResult Home()
        {
            return PartialView("~/Views/Home/_Home.cshtml");
        }
        public ActionResult Login()
        {
            return PartialView("~/Views/Login/_Login.cshtml");
        }
        public ActionResult Register()
        {
            return PartialView("~/Views/Login/_Register.cshtml");
        }
        public ActionResult ForgotPassword()
        {
            return PartialView("~/Views/Login/_ForgotPassword.cshtml");
        }
        public ActionResult Error()
        {
            return PartialView("~/Views/Error/_Error.cshtml");
        }
        public ActionResult Discussion()
        {
            return PartialView("~/Views/Discussion/_Discussion.cshtml");
        }
        public ActionResult RegisterSuccess()
        {
            return PartialView("~/Views/Login/_RegisterSuccess.cshtml");
        }
        public ActionResult VerifyAccount()
        {
            return PartialView("~/Views/Login/_VerifyAccount.cshtml");
        }
        public ActionResult CreateDiscussion()
        {
            return PartialView("~/Views/Discussion/_CreateDiscussion.cshtml");
        }
        public ActionResult ThreadDetail()
        {
            return PartialView("~/Views/Discussion/_DiscussionDetail.cshtml");
        }
        public ActionResult Event()
        {
            return PartialView("~/Views/Event/_Event.cshtml");
        }
        public ActionResult EventDetail()
        {
            return PartialView("~/Views/Event/_EventDetail.cshtml");
        }
        public ActionResult Search()
        {
            return PartialView("~/Views/Search/_Search.cshtml");
        }
        public ActionResult Profile()
        {
            return PartialView("~/Views/User/_Profile.cshtml");
        }
		public ActionResult CreateEvent()
        {
            EventTypeDropList eventTypeDropList = new EventTypeDropList();
            using (var db = new EventDAL())
            {
                eventTypeDropList = db.GetEventType();
            }
		    if (eventTypeDropList.EventTypeList.Count <= 0)
		    {
                return PartialView("~/Views/Error/_Error.cshtml");
		    }
            return PartialView("~/Views/Event/_CreateEvent.cshtml", eventTypeDropList);
        }

        public ActionResult Organization()
        {
            return PartialView("~/Views/Organization/_Organization.cshtml");
        }
        public ActionResult OrganizationDetail()
        {
            return PartialView("~/Views/Organization/_OrganizationDetail.cshtml");
        }
        public ActionResult CreateOrganization()
        {
            return PartialView("~/Views/Organization/_CreateOrganization.cshtml");
        }
    }
}