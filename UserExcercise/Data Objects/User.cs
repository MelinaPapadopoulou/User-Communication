using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;



namespace UserExcercise
{
    public enum Privilege { admin, user, guest, disabled }
    class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public Privilege UsersPrivilege { get; set; }
        public string Password { get; set; }

        public IList<ForumMessage> ForumMessages { get; set; }
        public IList<PersonalMessage> SentMessages { get; set; }
        public IList<PersonalMessage> RecievedMessages { get; set; }

        public User()
        {
            ForumMessages = new List<ForumMessage>();
            RecievedMessages = new List<PersonalMessage>();
            SentMessages = new List<PersonalMessage>();
        }
    }
}
