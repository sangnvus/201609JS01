using System;
using System.Web.Mvc;
using API_NganLuong;
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
        public ActionResult Messages()
        {
            return PartialView("~/Views/User/_Message.cshtml");
        }
        public ActionResult Notification()
        {
            return PartialView("~/Views/User/_Notification.cshtml");
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
        public ActionResult Donate()
        {
            return PartialView("~/Views/Donation/_Donate.cshtml");
        }
        public ActionResult DonationComplete()
        {
            try
            {
                if (Session["DonatedToken"] == null || Session["DonatedInfo"] == null)
                {
                    return null;
                }
                else
                {
                    var newDonate = (DonationDTO)Session["DonatedInfo"];
                    String Token = Session["DonatedToken"].ToString();
                    RequestCheckOrder info = new RequestCheckOrder();
                    info.Merchant_id = "48283";
                    info.Merchant_password = "12ba1130cf119352596dc8e1ba8e5fbf";
                    info.Token = Token;
                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
                    using (var db = new DonationDAL())
                    {
                        db.AddNewDonation(newDonate);
                    }
                    Session.Remove("DonatedInfo");
                    Session.Remove("DonatedToken");
                    //string show = result.errorCode + result.payerName;
                    return PartialView("~/Views/Donation/_DonationDone.cshtml");
                }
            }
            catch (Exception)
            {
                return PartialView("~/Views/Error/_Error.cshtml");
            }
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
        public ActionResult EditOrganization()
        {
            return PartialView("~/Views/Organization/_EditOrganization.cshtml");
        }
    }
}