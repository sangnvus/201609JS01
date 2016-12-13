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
    public class ConservationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddConservation(ConservationBasicInfoDTO newConservation)
        {
            int receiverId = 0;
            int CurrentUser = 0;
            Conversation returnedConservation;
       try {
            using (var db = new UserDAL())
            {
               CurrentUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
                receiverId = db.GetUserByUserNameOrEmail(newConservation.ReceiverName).UserID;
            }
            //Add conservation
            var conservation = new Conversation
            {
                CreatorId = CurrentUser,
                ReceiverId = receiverId,
                Title = newConservation.Title,
                UpdatedTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                Status = true,
                IsCreatorRead = true,
                IsReceiverRead = false,
            };
            using (var db = new ConservationDAL())
            {
                returnedConservation=db.AddNewConservation(conservation);
            }
            //Add First Message of Conservation
            var message = new Message
            {
                UserId = CurrentUser,
                ConservationId = returnedConservation.ConservationId,
                Content = newConservation.Content,
                CreatedDate = DateTime.Now,
                Status = true
            };
            using (var db = new ConservationDAL())
            {
                db.AddNewMessage(message);
            }}
      catch (Exception)
            {
                return Redirect("/#/Error");
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });
        }

        [HttpGet]
        public IHttpActionResult GetAllConservation()
        {
            List<ConservationBasicInfoDTO> ConservationList;
            using (var db = new ConservationDAL())
            {
                ConservationList = db.GetAllConservationByUserId(User.Identity.Name);
            }
            if (ConservationList != null)
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = ConservationList });
            else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
        }
        [HttpGet]
        public IHttpActionResult GetConservationByStatus(bool status)
        {
            List<ConservationBasicInfoDTO> ConservationList;
            using (var db = new ConservationDAL())
            {
                ConservationList = db.GetAllConservationByUserIdAndStatus(User.Identity.Name, status);
            }
            if (ConservationList != null)
                return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = ConservationList });
            else return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.NOT_FOUND });
        }
        [HttpGet]
        public IHttpActionResult GetAllMessageByConservationId(int conservationId)
        {
            List<MessageBasicInfoDTO> MessageList;
            using (var db = new ConservationDAL())
            {
                MessageList = db.GetAllMessageByConservationId(conservationId, User.Identity.Name);
            }
            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS, Data = MessageList });

        }
        [HttpGet]
        public IHttpActionResult AddMessage(int ConservationId, string newMessage)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(User.Identity.Name).UserID;
            }
            Message mess = new Message();
            mess.ConservationId = ConservationId;
            mess.Content = newMessage;
            mess.CreatedDate = DateTime.Now;
            mess.UserId = CurrenUser;
            mess.Status = true;
            using (var db = new ConservationDAL())
            {
                db.AddNewMessage(mess);
                Conversation current = db.GetConservationById(ConservationId);
                current.UpdatedTime = DateTime.Now;
                db.UpdateTime(current);
            }
            //Update Updated Time for Conservation

            return Ok(new HTTPMessageDTO { Status = WsConstant.HttpMessageType.SUCCESS });

        }

    }
}
