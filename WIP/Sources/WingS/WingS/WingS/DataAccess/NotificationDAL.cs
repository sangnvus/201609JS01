using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.DataAccess
{
    public class NotificationDAL : IDisposable
    {
        public List<NotificationBasicInfoDTO> GetAllNotificationByUserName(string UserName)
        {
            List<NotificationBasicInfoDTO> returnedList = new List<NotificationBasicInfoDTO>();
            using (var db = new Ws_DataContext())
            {
                var returnedConservation = db.Activity.OrderByDescending(x=>x.User.UserName==UserName&&x.Status==true).ToList();
                foreach(var item in returnedConservation)
                {
                    NotificationBasicInfoDTO itemInfo = new NotificationBasicInfoDTO();
                    itemInfo.CreatorName = item.CreatorName;
                    if (DateTime.Now.Subtract(item.CreatedDate).Hours <= 24 && DateTime.Now.Subtract(item.CreatedDate).Hours >= 1)
                        itemInfo.CreatedDate = DateTime.Now.Subtract(item.CreatedDate).Hours + " Tiếng cách đây";
                    else if (DateTime.Now.Subtract(item.CreatedDate).Hours > 24)
                        itemInfo.CreatedDate = item.CreatedDate.ToString("H:mm:ss dd/MM/yy");
                    else itemInfo.CreatedDate = DateTime.Now.Subtract(item.CreatedDate).Minutes + " Phút cách đây";
                    itemInfo.Content = item.Message;
                    itemInfo.ImageUrl = item.CreatorImage;
                    itemInfo.NotifyUrl = item.NotifyUrl;
                    returnedList.Add(itemInfo);
                }
            }
            return returnedList;
        }
        public Activity AddNotification(Activity newNotification)
        {
            using (var db = new Ws_DataContext())
            {
                newNotification = db.Activity.Add(newNotification);
                db.SaveChanges();
            }
            return newNotification;
        }
        public void Dispose()
        {

        }
    }
}