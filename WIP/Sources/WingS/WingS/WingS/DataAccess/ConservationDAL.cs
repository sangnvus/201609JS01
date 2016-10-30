using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;
namespace WingS.DataAccess
{
    public class ConservationDAL : IDisposable
    {
       public Conservation AddNewConservation(Conservation newConservation)
        {
            using (var db = new Ws_DataContext())
            {
                var returnedConservation = db.Conservation.Add(newConservation);
                db.SaveChanges();
                return returnedConservation;
            }
        }
        public Message AddNewMessage (Message newMessage)
        {
            using (var db = new Ws_DataContext())
            {
                var returnedMessage = db.Message.Add(newMessage);
                db.SaveChanges();
                return returnedMessage;
            }
        }
        public void Dispose()
        {

        }

    }
}