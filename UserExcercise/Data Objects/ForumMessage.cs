using System;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserExcercise
{
    class ForumMessage
    {
        public int ForumMessageID { get; set; }
        public int SenderID { get; set; }
        public DateTime DateCreated { get; set; }
        public string MessageText { get; set; }

        public virtual User Sender {get; set;}

        public ForumMessage()
        {
            DateCreated = DateTime.Now;

        }
    }
}
