namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CommentEvent
    {
        [Key]
        public int CommentEventId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public bool Status { get; set; }
        public virtual Event Event { get; set; }
        public virtual Ws_User Ws_User { get; set; }
        public ICollection<SubCommentEvent> SubComment { get; set; }
        public ICollection<LikeCommentEvent> LikeComment { get; set; }
    }
}
