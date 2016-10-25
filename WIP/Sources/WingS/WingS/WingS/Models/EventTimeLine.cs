using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WingS.Models
{
    public class EventTimeLine
    {
        [Key]
        public int TimeLineId { get; set; }
        [ForeignKey("evt")]
        public int EventId { get; set; }
        public string Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Status { get; set; }
        public virtual Event evt { get;set; }
    }
}