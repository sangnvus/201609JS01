using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Connection
    {
        [Key]
        public int ConnectionId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string ConnectionString { get; set; }
        public virtual Ws_User User { get; set; }
    }
}