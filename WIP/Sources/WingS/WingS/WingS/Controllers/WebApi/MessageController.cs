using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.Models.DTOs;
using WingS.DataAccess;
using WingS.Models;
using WingS.DataHelper;
namespace WingS.Controllers.WebApi
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddMessage(AddMessageDTO message)
        {
            int receiverId = 0;
            using (var db = new UserDAL())
            {
                receiverId = db.GetUserByUserNameOrEmail(message.ReceiverName).UserID;
            }
            var newMessage = new Message
            {
                CreatorId = WsConstant.CurrentUser.UserId,
                ReceiverId = receiverId,
                Content = message.Content,
                Title = message.Title,
                CreatedDate = DateTime.Now,
                Status = true,
                isHidden = false,
                isRead = false,
             };
            using (var db = new MessageDAL())
            {
                db.AddMessage(newMessage);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS});
        }
    }
}
