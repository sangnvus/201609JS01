using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorImage { get; set; }
        public string NotifyUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool Status { get; set; }
        public virtual Ws_User User { get; set; }
    }
}