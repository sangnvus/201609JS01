using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models;

namespace WingS.DataAccess
{
    public class WsData_Initializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<Ws_DataContext>
    {
        protected override void Seed(Ws_DataContext context)
        {
            //Initiate Dummy Data
            var User = new List<Ws_User>
            {
            new Ws_User{UserName="nhienlh",UserPassword="123456",AccountType=true,IsActive=true,CreatedDate=DateTime.Now,
            LastLogin =DateTime.Now,Email="blacksnow055@gmail.com",VerifyCode="AAAAAA" },
            new Ws_User{UserName="nghiadt",UserPassword="123456",AccountType=true,IsActive=true,CreatedDate=DateTime.Now,
            LastLogin =DateTime.Now,Email="blacksnow055@gmail.com",VerifyCode="AAAAAA" },
            new Ws_User{UserName="duytn",UserPassword="123456",AccountType=true,IsActive=true,CreatedDate=DateTime.Now,
            LastLogin =DateTime.Now,Email="blacksnow055@gmail.com",VerifyCode="AAAAAA" },
            new Ws_User{UserName="nhienlh",UserPassword="123456",AccountType=true,IsActive=true,CreatedDate=DateTime.Now,
            LastLogin =DateTime.Now,Email="Blacksnow055@gmail.com",VerifyCode="AAAAAA" }
            };
            context.SaveChanges();
        }
    }
}