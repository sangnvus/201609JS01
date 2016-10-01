using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class EventAlbumImage
    {

        [Key]
        public int ImageId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public string ImageUrl { get; set; }
        public bool? status { get; set; }
        public virtual Event Event { get; set; }
    }
}