using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Conversation
    {
        [Key]
        public int ConservationId { get; set; }
        public int CreatorId { get; set; }
        public int ReceiverId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsCreatorRead { get; set; }
        public bool IsReceiverRead { get; set; }
        public bool Status { get; set; }
        public ICollection<Message> Message { get; set; }
        public virtual Ws_User Creator { get; set; }
        public virtual Ws_User Receiver { get; set; }
    }
}