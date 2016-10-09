using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class SubCommentThread
    {
        [Key]
        public int SubCommentThreadId { get; set; }
        [ForeignKey("CommentThread")]
        public int CommentThreadId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public bool Status { get; set; }
        public virtual CommentThread CommentThread { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}