namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Organization
    {
        [Key]
        [ForeignKey("Ws_User")]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string EOrganizationName { get; set; }
        public string Introduction { get; set; }
        public string LogoUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerify { get; set; }
        public int Point { get; set; }
        public DateTime CreatedDate { get; set; } 
        public virtual ICollection<Event> Events { get; set; }
        public virtual Ws_User Ws_User { get; set; }
    }
}
