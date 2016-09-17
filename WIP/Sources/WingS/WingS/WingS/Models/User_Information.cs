using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class User_Information
    {
        [Key]
        [ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public DateTime DoB { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string Country { get; set; }
        public string FacebookUrl { get; set; }
        public string OrgnazationIDFollow { get; set; }
        public string UserSignature { get; set; }
        public int Point { get; set; }
        #region Relationship
     
        public virtual Ws_User User { get; set; }
        #endregion
    }
}