using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class AddCommentDTO
    {
       public int ThreadId { get; set; }
        public string CommentContent { get; set; }
    }
}