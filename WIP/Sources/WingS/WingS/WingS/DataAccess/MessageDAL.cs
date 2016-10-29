using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;
namespace WingS.DataAccess
{
    public class MessageDAL : IDisposable
    {
       public MessageBasicInfoDTO AddMessage(Message newMessage)
        {
            MessageBasicInfoDTO Message = new MessageBasicInfoDTO();
            
            using (var db = new Ws_DataContext())
            {
                db.Messages.Add(newMessage);
                db.SaveChanges();
            }
                return Message;
        }
        public void Dispose()
        {

        }

    }
}