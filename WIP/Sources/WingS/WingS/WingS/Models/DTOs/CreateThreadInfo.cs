using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Models.DTOs
{
    public class CreateThreadInfo
    {
        public int CreatorID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Content { get; set; }

    }
}