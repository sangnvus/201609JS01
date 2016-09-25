namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Information
    {
        [Key]
        [ForeignKey("Ws_User")]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public DateTime? DoB { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string FacebookUrl { get; set; }
        public string OrgnazationIDFollow { get; set; }
        public string UserSignature { get; set; }
        public int Point { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}
