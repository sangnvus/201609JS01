using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class MessageBasicInfoDTO
    {
        private string CreatorName { get; set; }
        private string ReceiverName{ get; set; }
        private string Content { get; set; }
        private bool isRead { get; set; }
        private bool isHidden { get; set; }
        private string SentDate { get; set; }
        public MessageBasicInfoDTO()
        {
            CreatorName = "";
            ReceiverName = "";
            Content = "";
            isRead = false;
            isHidden = false;
            SentDate = DateTime.Now.ToString();
        }
    }
}