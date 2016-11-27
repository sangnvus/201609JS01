using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class BasicCommentThread
    {
        public int UserCommentedId { get; set; }
        public string UserCommentedName { get; set; }
        public string UserImageProfile { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string CommentedTime { get; set; }
        public int NumberSubComment { get; set; }
        public int NumberOfLikes { get; set; }
        public bool isLiked { get; set; }
        public BasicCommentThread()
        {
            NumberSubComment = 0;
            UserCommentedId = 0;
            UserCommentedName = "";
            Content = "";
            CommentedTime = "";
            UserImageProfile = "";
        }


    }
}