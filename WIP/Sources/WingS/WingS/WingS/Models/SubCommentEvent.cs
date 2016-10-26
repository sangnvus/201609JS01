using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class SubCommentEvent
    {

        [Key]
        public int SubCommentEventId { get; set; }
        [ForeignKey("CommentEvent")]
        public int CommentEventId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public bool Status { get; set; }
        public virtual CommentEvent CommentEvent { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}