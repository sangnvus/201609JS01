using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class CreateThreadInfo
    {
        public int CreatorID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}