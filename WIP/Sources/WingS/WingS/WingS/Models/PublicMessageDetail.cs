using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class PublicMessageDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }
        public bool Status { get; set; }
        public virtual Ws_User User { get; set; }
        public virtual Event Event { get; set; }
    }
}