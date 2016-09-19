namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CommentThread
    {
        [Key]
        public int CommentThreadId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public bool Status { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}
