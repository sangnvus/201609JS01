namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ws_User
    {

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
        public virtual ICollection<CommentEvent> CommentEvents { get; set; }
        public virtual ICollection<CommentThread> CommentThreads { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual Organazation Organazation { get; set; }
        public virtual ICollection<ReportEvent> ReportEvents { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual User_Information User_Information { get; set; }
    }
}
