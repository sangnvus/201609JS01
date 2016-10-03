﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class EventBasicInfo
    {
        public int EventID { get; set; }
        public int CreatorID { get; set; }
        public string EventName { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public EventBasicInfo()
        {
            EventID = 0;
            CreatorID = 0;
            EventName = "";
            Content = "";
            ImageUrl = "";
            Status = false;
        }
    }
}