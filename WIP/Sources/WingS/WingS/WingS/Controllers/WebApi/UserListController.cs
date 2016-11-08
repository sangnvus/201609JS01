using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers.WebApi
{
    public class UserListController : ApiController
    {
        // Get list thread by create date
        [HttpGet]
        [ActionName("GetAllUser")]
        public IHttpActionResult GetAllUser()
        {
            List<Ws_User> listUser = new List<Ws_User>();
            try
            {
                using (var db = new UserDAL())
                {
                    listUser = db.GetAllUser();
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = listUser });
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }
    }
}
