using System;
using System.Web;
using System.Web.Http;
using API_NganLuong;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class DonationDoneController : ApiController
    {
        [HttpGet]
        public IHttpActionResult AddNewDonation()
        {
            try
            {

                if (HttpContext.Current.Session["DonatedToken"] == null || HttpContext.Current.Session["DonatedInfo"] == null)
                {
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.BAD_REQUEST });
                }
                else
                {
                    var newDonate = (DonationDTO)HttpContext.Current.Session["DonatedInfo"];
                    String Token = HttpContext.Current.Session["DonatedToken"].ToString();
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
                    HttpContext.Current.Session.Remove("DonatedInfo");
                    HttpContext.Current.Session.Remove("DonatedToken");
                    return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
                }
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR});
            }
        }
        [HttpGet]
        public IHttpActionResult GetDonateInfo(int eventId)
        {
            var userInfo = new UserBasicInfoDTO();
            var newDonate = new DonationDTO();
            using (var db = new UserDAL())
            {
                //User.Identity.Name
                var user = db.GetUserByUserNameOrEmail(User.Identity.Name);
                var userGet = db.GetUserInformation(user.UserID);
                userInfo.FullName = userGet.FullName;
                userInfo.Email = user.Email;
                userInfo.Phone = userGet.Phone;
                newDonate.UserId = user.UserID;
                newDonate.EventId = eventId; 
            }
            HttpContext.Current.Session["DonatedInfo"] = newDonate;
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = userInfo });
        }
    }
}
