using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Ws_User
    { 
        #region Attribute
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool AccountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string Email { get; set; }
        public string VerifyCode { get; set; }
        #endregion
        #region RelationShip
        public virtual User_Information User_Info { get; set; }
        public virtual Organazation Orgnazation { get; set; }

        #endregion
    }
}