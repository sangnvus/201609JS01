using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class AddMessageDTO
    {
        public string ReceiverName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}