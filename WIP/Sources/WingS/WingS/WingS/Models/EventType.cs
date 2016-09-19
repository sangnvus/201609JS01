namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventType
    {
        [Key]
        public int EventTypeID { get; set; }
        public string EventName { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
