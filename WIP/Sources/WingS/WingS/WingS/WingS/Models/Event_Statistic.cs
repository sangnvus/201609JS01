namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event_Statistic
    {
        [Key]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public virtual Event Event { get; set; }
    }
}
