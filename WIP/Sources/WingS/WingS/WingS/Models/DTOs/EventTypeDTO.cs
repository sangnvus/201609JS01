using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class EventTypeDTO
    {
        public int EventTypeID { get; set; }
        public string EventTypeName { get; set; }
        public string Content { get; set; }
        public int NumberRelatedEvent { get; set; }
        public bool IsActive { get; set; }

        public EventTypeDTO()
        {
            EventTypeID = 0;
            EventTypeName = "";
            Content = "";
            NumberRelatedEvent = 0;
            IsActive = false;
        }
    }
}