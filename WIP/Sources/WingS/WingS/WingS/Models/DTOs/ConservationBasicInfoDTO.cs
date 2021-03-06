﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class ConservationBasicInfoDTO
    {
        public int ConservationId { get; set; }
        public string CreatorName { get; set;}
        public string ReceiverName { get; set; }
        public string AvatarUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedDate { get; set; }
        public bool isRead { get; set; }
        public bool isHidden { get; set; }
    }
}