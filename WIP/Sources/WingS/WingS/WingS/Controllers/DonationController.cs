using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using API_NganLuong;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class DonationController : Controller
    {
        /// <summary>
        /// Get info then redirect to NganLuong
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public ActionResult CheckOutNganLuong(NganLuongCheckOut inputData)
        {
            //get data from form
            string payment_method = inputData.option_payment;
            string str_bankcode = inputData.bankcode;
            bool isPublic = inputData.isPublic != "0";
            var numberofMoney = inputData.numberMoney == "0" ? inputData.inputMoney : inputData.numberMoney;
            //set data to nganluongAPI
            RequestInfo info = new RequestInfo
            {
                Merchant_id = WsConstant.NganLuongApi.MerchantId,
                Merchant_password = WsConstant.NganLuongApi.Password,
                Receiver_email = WsConstant.NganLuongApi.AdminEmail,
                cur_code = "vnd",
                bank_code = str_bankcode,
                Order_code = "chuyen_khoan_ung_ho",
                Total_amount = numberofMoney,
                fee_shipping = "0",
                Discount_amount = "0",
                order_description = "Chuyển tiền ủng hộ thông qua Ngân Lượng",
                return_url = "http://localhost:2710/#/DonationComplete",
                cancel_url = "http://localhost:2710/#/DonationFailed",
                Buyer_fullname = inputData.buyer_fullname,
                Buyer_email = inputData.buyer_email,
                Buyer_mobile = inputData.buyer_mobile
            };
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
            //get and set data to session
            var newDonate = new DonationDTO();
            newDonate.IsPublic = isPublic;
            newDonate.DonatedMoney = decimal.Parse(numberofMoney);
            newDonate.TradeCode = result.Token;
            newDonate.UserId = inputData.DonateUserId;
            newDonate.EventId = inputData.DonateEventId;
            newDonate.Content = inputData.DonateContent;
            Session["DonatedInfo"] = newDonate;
            //return to checkout page or error page
            if (result.Error_code == "00")
            {
                return Redirect(result.Checkout_url);
                //return Redirect("http://localhost:2710/#/DonationComplete");
                //return Redirect("http://localhost:2710/#/DonationFailed");
            }
            else
            {
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        /// <summary>
        /// Check Event state to donate
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public JsonResult CheckDonateEvent(int eventId)
        {
            using (var db = new EventDAL())
            {
                var eventGet = db.GetEventById(eventId);
                if (!eventGet.Status || DateTime.Now > eventGet.Finish_Date)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }
	}
}