using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WingS.DataHelper;
using WingS.DataAccess;
using WingS.Models.DTOs;
using WingS.Models;
namespace WingS.ChatHub
{
    public class GlobalHub : Hub
    {
        public void Connect()
        {
            var ConnectionString = Context.ConnectionId;
            int UserId = WsConstant.CurrentUser.UserId;
            //Check User is connected or not
            using (var db = new Ws_DataContext())
            {
                var result = db.Connection.SingleOrDefault(x => x.UserId == UserId);
                if(result!= null)
                {
                    db.Connection.Remove(result);
                }
                var Users = db.Connection.ToList();
                Connection UserDetail = new Connection
                    {
                        UserId = UserId,
                        ConnectionString = ConnectionString
                    };
                db.Connection.Add(UserDetail);
                db.SaveChanges();
            }
            //Send message to caller
            Clients.Caller.Notify("Successfull");
           
        }
        public void SendMessage(int ConservationId, string Message )
        {
            Message newMess = new Message();
            using (var db = new Ws_DataContext())
            {
                newMess.ConservationId = ConservationId;
                newMess.Content = Message;
                newMess.CreatedDate = DateTime.Now;
                newMess.Status = true;
                newMess.UserId = WsConstant.CurrentUser.UserId;
                newMess = db.Message.Add(newMess);
                db.SaveChanges();
            }
            MessageBasicInfoDTO info = new MessageBasicInfoDTO();
            info.CreatorImage = newMess.User.User_Information.ProfileImage;
            info.CreatorName = newMess.User.UserName;
            info.Content = newMess.Content;
            if (DateTime.Now.Subtract(newMess.CreatedDate).Hours <= 24 && DateTime.Now.Subtract(newMess.CreatedDate).Hours >= 1)
                info.CreatedDate = DateTime.Now.Subtract(newMess.CreatedDate).Hours + " Tiếng cách đây";
            else if (DateTime.Now.Subtract(newMess.CreatedDate).Hours > 24)
                info.CreatedDate = newMess.CreatedDate.ToString("H:mm:ss dd/MM/yy");
            else info.CreatedDate = DateTime.Now.Subtract(newMess.CreatedDate).Minutes + " Phút cách đây";

            //Send new Message to Receiver 
            
            //Send new Message to Caller 
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (var db = new Ws_DataContext())
            {
                var item = db.Connection.FirstOrDefault(x => x.ConnectionString == Context.ConnectionId);
                if (item != null)
                {
                    db.Connection.Remove(item);
                    db.SaveChanges();
                }
            }
            return base.OnDisconnected(stopCalled);
        }

    }

   
   
}
