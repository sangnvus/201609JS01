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
    public class AdminUserListController : ApiController
    {
        // Get list thread by create date
        [HttpGet]
        [ActionName("GetAllUser")]
        public IHttpActionResult GetAllUser()
        {
            List<UserBasicDTO> listUser = new List<UserBasicDTO>();
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

        [HttpGet]
        [ActionName("ChangeStatusUser")]
        public IHttpActionResult ChangeStatusUser(int userid)
        {
            try
            {
                bool statusUser;
                using (var userDal = new UserDAL())
                {
                    var user = userDal.GetUserById(userid);
                    user.IsActive = !user.IsActive;
                    statusUser = user.IsActive;
                    userDal.UpdateUser(user);
                }
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = statusUser });
            }
            catch (Exception)
            {
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.ERROR });
            }
        }
    }
}
