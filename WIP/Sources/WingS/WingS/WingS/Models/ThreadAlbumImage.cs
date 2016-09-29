using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WingS.Models
{
    public class ThreadAlbumImage
    {

        [Key]
        public int ImageId { get; set; }
        [ForeignKey("thread")]
        public int ThreadId { get; set; }
        public string ImgaeUrl { get; set; }
        public bool? status { get; set;}
        public virtual Thread thread { get; set; }
    }
}