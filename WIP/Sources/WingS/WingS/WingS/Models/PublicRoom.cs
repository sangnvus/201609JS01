using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class PublicRoom
    {
        [Key]
        public int PublicRoomId { get; set; }
        [ForeignKey("ConnectionRoom")]
        public int ConnectionId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set;}
        public virtual Connection ConnectionRoom { get; set; }
        public virtual Event Event { get; set; }
    }
}