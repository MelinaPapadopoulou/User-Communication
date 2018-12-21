using System;
using System.Collections.Generic;
using System.Linq;

namespace UserExcercise
{
    class ForumMessageAction : MessageAction
    {
        public ForumMessageAction(IProvideData dataprovider, User LoggedInUser) : base(dataprovider, LoggedInUser) { }

        public void Send()
        {
            ForumMessage message = new ForumMessage()
            {
                SenderID = ActiveUser.UserId,
                MessageText = WriteMessage("Body: ", 250)
            };
            DataProvider.CreateForumMessage(message);

        }

        public string ShowPrevious()
        {
            List<ForumMessage> ListOfForumMessages = DataProvider.GetLastForumMessages();
            ListOfForumMessages.Reverse();
            return string.Join("\n", ListOfForumMessages.Select(fm => { return $"{fm.Sender.Username}: {fm.MessageText}"; }));
        }



    }
}
