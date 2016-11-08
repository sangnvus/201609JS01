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
            var basicUserList = new List<UserBasicDTO>();
            try
            {
                using (var db = new UserDAL())
                {
                    listUser = db.GetAllUser();
                }
                foreach (var e in listUser)
                {
                    basicUserList.Add(new UserBasicDTO
                    {
                        UserId = e.UserID,
                        UserName = e.UserName,
                        AccountType = e.AccountType,
                        IsActive = e.IsActive,
                        IsVerify = e.IsVerify,
                        CreatedDate = e.CreatedDate.ToString("dd/MM/yyyy"),
                        Email = e.Email
                    });
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = basicUserList});
            }
            catch (Exception)
            {
                //ViewBag.ErrorMessage = ex;
                return Redirect("/#/Error");
            }

        }
    }
}
