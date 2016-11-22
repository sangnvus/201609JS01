using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using API_NganLuong;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Donation/
        public ActionResult CheckOutNganLuong(NganLuongCheckOut inputData)
        {
            string payment_method = inputData.option_payment;
            string str_bankcode = inputData.bankcode;
            string numberofMoney;
            if (inputData.numberMoney == "0")
            {
                numberofMoney = inputData.inputMoney;
            }
            else
            {
                numberofMoney = inputData.numberMoney;
            }
            

            RequestInfo info = new RequestInfo
            {
                Merchant_id = "48283",
                Merchant_password = "12ba1130cf119352596dc8e1ba8e5fbf",
                Receiver_email = "anhtuanvt93@gmail.com",
                cur_code = "vnd",
                bank_code = str_bankcode,
                Order_code = "ma_don_hang01",
                Total_amount = numberofMoney,
                fee_shipping = "0",
                Discount_amount = "0",
                order_description = "Chuyển tiền ủng hộ thông qua Ngân Lượng",
                return_url = "http://localhost:2710/#/DonationComplete",
                cancel_url = "http://localhost:2710/#/DonationFailed",
                /*Buyer_fullname = inputData.buyer_fullname,
                Buyer_email = inputData.buyer_email,
                Buyer_mobile = inputData.buyer_mobile*/
            };
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);

            if (result.Error_code == "00")
            {
                return Redirect(result.Checkout_url);
            }
            else
            {
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        protected string convert_utf8(string str)
        {

            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(str);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = utf8.GetString(isoBytes);
            return msg;
        }
	}
}