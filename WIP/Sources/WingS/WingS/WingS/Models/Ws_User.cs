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
        public bool IsVerify { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Email { get; set; }
        public string VerifyCode { get; set; }
        public virtual ICollection<CommentEvent> CommentEvents { get; set; }
        public virtual ICollection<CommentThread> CommentThreads { get; set; }
        public virtual ICollection<SubCommentThread> SubcommentThreads { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual User_Information User_Information { get; set; }
        public virtual Organization Organazation { get; set; }
        public virtual ICollection<Conversation> CreatorConservation { get; set; }
        public virtual ICollection<Conversation> ReceiverConservation { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<PublicMessageDetail> PublicMessage{ get; set; }
        public virtual ICollection<Connection> Connection { get; set; }
        public virtual ICollection <LikeCommentEvent> LikeCommentEvent { get; set; }
    }
}
