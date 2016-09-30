using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ThreadAlbumImageDTO
    {
        public int ThreadId { get; set; }
        public string ImageUrl { get; set; }
        public ThreadAlbumImageDTO(int threadId, string imgUrl)
        {
            ThreadId = threadId;
            ImageUrl = imgUrl;
        }
    }
}