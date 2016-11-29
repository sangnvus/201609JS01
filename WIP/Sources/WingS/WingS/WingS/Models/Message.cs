using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [ForeignKey("ConserVation")]
        public int ConservationId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public virtual Conversation ConserVation { get; set; }
        public virtual Ws_User User { get; set;}
    }
}