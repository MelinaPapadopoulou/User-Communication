using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExcercise
{
    class PersonalMessage
    {
        public int PersonalMessageId { get; set; }
        public int RecieverID { get; set; }
        public int SenderID { get; set; }
        public DateTime DateCreated { get; set; }
        public string MessageText { get; set; }
        public bool IsMessageShownToReciever { get; set; }
        public bool IsMessageShownToSender { get; set; }
        public bool IsMessageRead { get; set; }
        public string MessageTitle { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }

        public PersonalMessage()
        {
            IsMessageRead = false;
            IsMessageShownToSender = true;
            IsMessageShownToReciever = true;
            DateCreated = DateTime.Now;
        }
    }

    public struct MessageContent
    {
        public string Title, Body;
    }
}
