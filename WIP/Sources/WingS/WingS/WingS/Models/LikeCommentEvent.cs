using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class LikeCommentEvent
    {
        [Key]
        public int LikeCommentId { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public bool Status { get; set; }
        public virtual CommentEvent Comment { get; set; }
        public virtual Ws_User User { get; set; }

    }
}