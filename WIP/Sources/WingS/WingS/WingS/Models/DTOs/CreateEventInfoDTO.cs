﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Models.DTOs
{
    public class CreateEventInfo
    {
        public int CreatorID { get; set; }
        public int EventType { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
    }
}