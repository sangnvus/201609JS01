namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class ReportEvent
    {
        [Key]
        public int ReportEventId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public string Reason { get; set; }
        public DateTime ReportTime { get; set; }
        public virtual Event Event { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}
