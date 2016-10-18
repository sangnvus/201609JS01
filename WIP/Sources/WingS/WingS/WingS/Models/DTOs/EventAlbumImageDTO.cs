using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class EventAlbumImageDTO
    {
        public int EventId { get; set; }
        public string ImageUrl { get; set; }
        public EventAlbumImageDTO(int eventId, string imgUrl)
        {
            EventId = eventId;
            ImageUrl = imgUrl;
        }
    }
}