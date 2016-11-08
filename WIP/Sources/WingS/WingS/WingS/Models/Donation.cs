namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Donation
    {
        [Key]
        public int DonationId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public decimal DonatedMoney { get; set; }
        public string DonatedDate { get; set; }
        public bool IsPublic { get; set; }
        public virtual Event Event { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}
