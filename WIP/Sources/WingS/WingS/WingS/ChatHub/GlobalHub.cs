using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WingS.DataHelper;
using WingS.DataAccess;
using WingS.Models.DTOs;
using WingS.Models;
using System.Data.Entity.Migrations;

namespace WingS.ChatHub
{
    public class GlobalHub : Hub
    {
        public void Connect()
        {
            ;
            var ConnectionString = Context.ConnectionId;
            int UserId = 0;
            using (var db = new UserDAL())
            {
                UserId = db.GetUserByUserNameOrEmail(HttpContext.Current.User.Identity.Name).UserID;
            }
            //Check User is connected or not
            using (var db = new Ws_DataContext())
            {
                Connection UserDetail = new Connection
                {
                    UserId = UserId,
                    ConnectionString = ConnectionString
                };
                db.Connection.Add(UserDetail);
                db.SaveChanges();
            }
           
        }
        //Join chat room
        public void JoinRoom(int eventId)
        {
            using (var db = new Ws_DataContext())
            {
                var item = db.Connection.OrderByDescending(x => x.ConnectionString.Equals(Context.ConnectionId)).FirstOrDefault();
                if(item!=null)
                {
                  var userdetails = new PublicRoom()
                {
                    EventId = eventId,
                    ConnectionId = item.ConnectionId                    
                };
                db.PublicRooms.Add(userdetails);
                db.SaveChanges();
                }

            }
        }
        //Disconnect chat room
        public void DisconnectRoom (int eventId)
        {
            using (var db = new Ws_DataContext())
            {
                var item = db.Connection.FirstOrDefault(x => x.ConnectionString.Equals(Context.ConnectionId));
                try {db.PublicRooms.Remove(db.PublicRooms.FirstOrDefault(x => x.ConnectionId == item.ConnectionId));
                }catch(Exception){}
            }
        }
        public void SendMessage(int ConservationId, string Message)
        {
            int UserId = 0;
            using (var db = new UserDAL())
            {
                UserId = db.GetUserByUserNameOrEmail(HttpContext.Current.User.Identity.Name).UserID;
            }
            Message newMess = new Message();
            MessageBasicInfoDTO info = new MessageBasicInfoDTO();
            using (var db = new Ws_DataContext())
            {
                newMess.ConservationId = ConservationId;
                newMess.Content = Message;
                newMess.CreatedDate = DateTime.Now;
                newMess.Status = true;
                newMess.UserId = UserId;
                newMess = db.Message.Add(newMess);
                db.SaveChanges();
                using (var dbCurrent = new ConservationDAL())
                {
                    Conversation current = dbCurrent.GetConservationById(ConservationId);
                    current.UpdatedTime = DateTime.Now;
                    if(UserId==current.CreatorId)
                    {
                        current.IsReceiverRead = false; 
                    }
                    if (UserId == current.ReceiverId)
                    {
                        current.IsCreatorRead = false;
                    }
                    dbCurrent.UpdateTime(current);
                }
                var GetInfo = (from p in db.Message
                               where p.MessageId == newMess.MessageId
                               select new { p.User.UserName, p.User.User_Information.ProfileImage }).SingleOrDefault();
                info.CreatorImage = GetInfo.ProfileImage;
                info.CreatorName = GetInfo.UserName;
                info.Content = newMess.Content;
            }
      
         
            if (DateTime.Now.Subtract(newMess.CreatedDate).Hours <= 24 && DateTime.Now.Subtract(newMess.CreatedDate).Hours >= 1)
                info.CreatedDate = DateTime.Now.Subtract(newMess.CreatedDate).Hours + " Tiếng cách đây";
            else if (DateTime.Now.Subtract(newMess.CreatedDate).Hours > 24)
                info.CreatedDate = newMess.CreatedDate.ToString("H:mm:ss dd/MM/yy");
            else info.CreatedDate = DateTime.Now.Subtract(newMess.CreatedDate).Minutes + " Phút cách đây";
            //Get list connection Id specify by UserId
            List<string> ListConnetion = new List<string>();
            using (var db = new Ws_DataContext())
            {
                //Get Id of Receiver 
                var Id = (from p in db.Conversation
                          where p.ConservationId == ConservationId
                          select new { p.CreatorId, p.ReceiverId }).SingleOrDefault();
                if (newMess.UserId == Id.CreatorId)
                {
                    ListConnetion = db.Connection.Where(x => x.UserId == Id.ReceiverId ).Select(x => x.ConnectionString).ToList();
                }
                else ListConnetion = db.Connection.Where(x => x.UserId == Id.CreatorId ).Select(x => x.ConnectionString).ToList();
                if (ListConnetion.Count() != 0)
                {
                    var currentConversation = db.Conversation.Where(x => x.ConservationId == ConservationId).SingleOrDefault();
                    if (UserId == currentConversation.CreatorId)
                    {

                        currentConversation.IsReceiverRead = true;
                    }
                    if (UserId == currentConversation.ReceiverId)
                    {
                        currentConversation.IsCreatorRead = true;

                    }
                    db.Conversation.AddOrUpdate(currentConversation);
                    db.SaveChanges();
                }
            }
            //Send it to caller
            Clients.Caller.ReceiverMessage(info);
            //Send new Message to Receiver if Connecting 
        
            foreach (var item in ListConnetion)
            {
                Clients.Client(item).NewMessageNotification("Bạn đã nhận 1 tin nhắn mới, xem tại Tin nhắn!");
                Clients.Client(item).ReceiverMessage(info);
            }


        }
        public void SendMessageInRoom(int eventId, string mess)
        {
            MessageBasicInfoDTO returnedMessage = new MessageBasicInfoDTO();
            int UserId = 0;
            using (var db = new UserDAL())
            {
                UserId = db.GetUserByUserNameOrEmail(HttpContext.Current.User.Identity.Name).UserID;
            }
            string currentConnection = Context.ConnectionId;
            using (var db = new Ws_DataContext())
            {
                //Add mesage to db
                PublicMessageDetail newMessage = new PublicMessageDetail();
                newMessage.UserId = UserId;
                newMessage.EventId = eventId;
                newMessage.Message = mess;
                newMessage.CreatedDate = DateTime.Now;
                newMessage.Status = true;
                newMessage = db.PublicMessageDetails.Add(newMessage);
                db.SaveChanges();
                //Set retured message
                returnedMessage.CreatorImage = db.User_Information.SingleOrDefault(x => x.UserID == newMessage.UserId).ProfileImage;
                returnedMessage.Content = newMessage.Message;
                returnedMessage.CreatedDate = newMessage.CreatedDate.ToString("H:mm dd/MM");
                returnedMessage.CreatorName = db.Ws_User.SingleOrDefault(x => x.UserID == newMessage.UserId).UserName;
            }
            //Send it to ALL clients is connecting this room
            using (var db = new Ws_DataContext())
            {
                var list = db.PublicRooms.Where(x => x.EventId == eventId)
                                         .Select(x=>x.ConnectionRoom.ConnectionString).ToList();
                foreach(var item in list)
                {
                    if(!item.Equals(currentConnection)) Clients.Client(item).ReceivePublicMessage(returnedMessage);

                }
            }
            //Send it to caller
            Clients.Caller.ReceivePublicMessage(returnedMessage);

        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            try { 
            using (var db = new Ws_DataContext())
            {
                var item = db.Connection.FirstOrDefault(x => x.ConnectionString == Context.ConnectionId);
                var connectItem = db.PublicRooms.FirstOrDefault(x => x.ConnectionId == item.ConnectionId);
                if (connectItem != null)
                {
                    db.PublicRooms.Remove(connectItem);
                    db.SaveChanges();
                }
                if (item != null)
                {
                    db.Connection.Remove(item);
                    db.SaveChanges();
                }
            }
            }catch(Exception ex)
            {

            }
            return base.OnDisconnected(stopCalled);
        }

    }

   
   
}
