using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class Report
    {

        [Key]
        public int ReportId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime ReportTime { get; set; }
        public string Type { get; set; }
        public int ReportTo { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedTime { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}