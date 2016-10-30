﻿using System;
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
            Conservation returnedConservation;
       try {
            using (var db = new UserDAL())
            {
                receiverId = db.GetUserByUserNameOrEmail(newConservation.ReceiverName).UserID;
            }
            //Add conservation
            var conservation = new Conservation
            {
                CreatorId = WsConstant.CurrentUser.UserId,
                ReceiverId = receiverId,
                Title = newConservation.Title,
                CreatedDate = DateTime.Now,
                Status = true,
                isHidden = false,
                isRead = false,
            };
            using (var db = new ConservationDAL())
            {
                returnedConservation=db.AddNewConservation(conservation);
            }
            //Add First Message of Conservation
            var message = new Message
            {
                UserId = WsConstant.CurrentUser.UserId,
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
    }
}