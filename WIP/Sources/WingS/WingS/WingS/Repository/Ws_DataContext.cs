using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WingS.Models;

namespace WingS.Repository
{
    public class Ws_Datacontext : DbContext
    {
      
        //create dbset to access database
        public DbSet<WS_User> WS_Users { get; set; }
       
    }


}