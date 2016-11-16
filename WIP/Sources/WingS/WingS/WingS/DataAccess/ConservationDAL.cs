﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        public Conservation GetConservationById(int Id)
        {
            using (var db = new Ws_DataContext())
            {
                var returnedConservation = db.Conservation.Where(x => x.ConservationId == Id).SingleOrDefault();
                return returnedConservation;
            }
        }
        public void UpdateTime(Conservation currentConservation)
        {
            using (var db = new Ws_DataContext())
            {
                db.Conservation.AddOrUpdate(currentConservation);
                db.SaveChanges();
            }
          
        }
        public List<MessageBasicInfoDTO> GetAllMessageByConservationId(int id)
        {
            List<MessageBasicInfoDTO> messageList = new List<MessageBasicInfoDTO>();
           
            using (var db = new Ws_DataContext())
            {

                var list = db.Message.OrderByDescending(x => x.ConservationId == id).ToList();
                foreach (var item in list)
                {
                    MessageBasicInfoDTO current = new MessageBasicInfoDTO();
                    //Set time
                    if (DateTime.Now.Subtract(item.CreatedDate).Hours <= 24 && DateTime.Now.Subtract(item.CreatedDate).Hours >= 1)
                        current.CreatedDate = DateTime.Now.Subtract(item.CreatedDate).Hours + " Tiếng cách đây";
                    else if (DateTime.Now.Subtract(item.CreatedDate).Hours > 24)
                        current.CreatedDate = item.CreatedDate.ToString("H:mm:ss dd/MM/yy");
                    else current.CreatedDate = DateTime.Now.Subtract(item.CreatedDate).Minutes + " Phút cách đây";
                    //Set other atributes
                    current.CreatorImage = item.User.User_Information.ProfileImage;
                    current.CreatorName = item.User.UserName;
                    current.Content = item.Content;
                    messageList.Add(current);
                }
            }
            return messageList;

        }
        public List<ConservationBasicInfoDTO> GetAllConservationByUserId(string name)
        {
            int CurrenUser = 0;
            using (var db = new UserDAL())
            {
                CurrenUser = db.GetUserByUserNameOrEmail(name).UserID;
            }
            List<ConservationBasicInfoDTO> ConservationList = new List<ConservationBasicInfoDTO>();
            using (var db = new Ws_DataContext())
            {
                var list = (from p in db.Conservation
                            where p.CreatorId == CurrenUser || p.ReceiverId == CurrenUser
                            select p).OrderByDescending(x=>x.UpdatedTime).ToList();
                foreach (var item in list)
                {
                    ConservationBasicInfoDTO current = new ConservationBasicInfoDTO();
                    //Set Image
                    if (item.ReceiverId == CurrenUser)
                    {
                        current.AvatarUrl = item.Creator.User_Information.ProfileImage;
                        current.CreatorName = item.Creator.UserName;
                    }
                    else
                    {
                        current.AvatarUrl = item.Receiver.User_Information.ProfileImage;
                        current.CreatorName = item.Receiver.UserName;
                    }
                    //Set Time
                    if (DateTime.Now.Subtract(item.UpdatedTime).Hours <= 24 && DateTime.Now.Subtract(item.UpdatedTime).Hours >= 1)
                        current.CreatedDate = DateTime.Now.Subtract(item.CreatedDate).Hours + " Tiếng cách đây";
                    else if (DateTime.Now.Subtract(item.UpdatedTime).Hours > 24)
                        current.CreatedDate = item.UpdatedTime.ToString("H:mm:ss dd/MM/yy");
                    else current.CreatedDate = DateTime.Now.Subtract(item.UpdatedTime).Minutes + " Phút cách đây";
                    //Set other attributes
                    current.Title = item.Title;
                    current.ConservationId = item.ConservationId;
                    ConservationList.Add(current);
                }
                return ConservationList;
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