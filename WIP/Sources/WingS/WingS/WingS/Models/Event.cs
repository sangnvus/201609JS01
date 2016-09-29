namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [ForeignKey("Organazation")]
        public int CreatorID { get; set; }
        [ForeignKey("EType")]
        public int EventType { get; set; }
        public string EventName { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Finish_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int TotalPoint { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<CommentEvent> CommentEvents { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<EventAlbumImage> Album { get; set; }

        public virtual EventType EType { get; set; }
        public virtual Organazation Organazation { get; set; }
        public virtual Event_Statistic Event_Statistic { get; set; }
        public virtual ICollection<ReportEvent> ReportEvents { get; set; }
    }
}
