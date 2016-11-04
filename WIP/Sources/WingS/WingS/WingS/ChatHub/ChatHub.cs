using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WingS.DataHelper;
using WingS.DataAccess;
using WingS.Models.DTOs;
using WingS.Models;
namespace WingS.ChatHub
{
    public class ChatHub : Hub
    {
        public static int UserIdLoaded = WsConstant.CurrentUser.UserId;
       
        //connect hub
        public void Connect(int userId, int eventId)
        {
            UserIdLoaded = userId;
            var connectionId = Context.ConnectionId;
            using (var db = new Ws_DataContext())
            {
                var item = db.PublicRooms.FirstOrDefault(x => x.UserId == userId);
                if (item != null)
                {
                    db.PublicRooms.Remove(item);
                    db.SaveChanges();

                }
                
                var user = db.PublicRooms.ToList();
                
                    var userdetails = new PublicRoom()
                    {
                        ConnectionId = connectionId,
                        UserId = userId,
                        EventId = eventId
                    };
                    db.PublicRooms.Add(userdetails);
                    db.SaveChanges();

            }
            Clients.Caller.Notify("OKKKKKKK");
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (var db = new Ws_DataContext())
            {
                var item = db.PublicRooms.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    db.PublicRooms.Remove(item);
                    db.SaveChanges();

                }
                return base.OnDisconnected(stopCalled);
            }
        }



       //send to all

        public void SendMessageToAll(int userId, int eventId, string message)
        {
            // store last 100 messages in cache
           AddAllMessageinCache(userId, message, eventId);

            //List connectionId in an Event
           
           //using (var db = new DbContext())
           //{

           //}

            Clients.All.messageReceived(userId, eventId, message);
            //AddAllMessageinCache(userName, message, eventId);
            //Get a list with only conectionId specify by Event call ListConnection
            //using (var db = new DbContext())
            //{
            //    //Get list connection id with condition eventId = eventId
            //}
            //// Broad cast message
            //for (int i = 0; i < ListConnection.lengh(); i++)
            //{
            //    Clients.Client(list[i]).messageReceived(userName, message);
            //}
        }
       

        //add to cache
        private void AddAllMessageinCache(int userId, string message, int eventId)
        {
            using (var db = new Ws_DataContext())
            {
                var publicMessageDetail = new PublicMessageDetail()
                {

                    UserId = UserIdLoaded,
                    EventId = eventId,                  
                    Message = message,
                  
                };
                db.PublicMessageDetails.Add(publicMessageDetail);
                db.SaveChanges();
            }
        }
    }
    

   
   
}
