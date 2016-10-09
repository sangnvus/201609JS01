using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class AddSubCommentDTO
    {
        public int CommentThreadId { get; set; }
        public string CommentContent { get; set; }
    }
}