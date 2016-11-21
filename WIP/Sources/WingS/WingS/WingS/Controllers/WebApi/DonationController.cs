using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            String Token = "";//Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = "48283";
            info.Merchant_password = "12ba1130cf119352596dc8e1ba8e5fbf";
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            using (var db = new DonationDAL())
            {
                db.AddNewDonation(new DonationDTO(1,1,1,true));
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = 1 });
        }
    }
}
