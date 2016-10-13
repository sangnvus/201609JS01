using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class LikeThreads
    {
        [Key]
        public int LikeId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public bool Status { get; set; }
        public virtual Ws_User User { get; set; }
        public virtual Thread Thread { get; set; }
    }
}