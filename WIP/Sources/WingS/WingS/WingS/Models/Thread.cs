namespace WingS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class Thread
    {
        [Key]
        public int ThreadId { get; set; }
        [ForeignKey("Ws_User")]
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<CommentThread> CommentThreads { get; set; }
        public virtual Ws_User Ws_User { get; set; }
        public virtual ICollection<ThreadAlbumImage> ImageAlbum { get; set; }
    }
}
