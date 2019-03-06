using System;
using System.Collections.Generic;
using System.Linq;

namespace UserExcercise
{
    class PersonalMessageAction : MessageAction
    {
        public PersonalMessageAction(IProvideData dataprovider, User activeuser) : base(dataprovider, activeuser) { }

        public string CheckForUnreadMessages()
        {
            int TotalUnreadPMs = DataProvider.ReadPersonalMessages(ActiveUser, IsUserSender: false).Count(PM => !PM.IsMessageRead);
            return (TotalUnreadPMs > 0) ? $" ({TotalUnreadPMs})" : "";
        }

        public void ShowRecievedMessages()
        {
            while (true)
            {
                // Get the messages
                List<PersonalMessage> PersonalMessages = GetRecievedMessages(IsUserSender: false);

                // User select one
                int indexOfSelectedMessage = SelectionMenu.Vertical(PresentPersonalMessages(PersonalMessages)).IndexOfChoice;

                // Mark it as read
                DataProvider.UpdateMessageAsRead(PersonalMessages[indexOfSelectedMessage].PersonalMessageId);

                // Choose action on READ message
                ManageMessage(indexOfSelectedMessage, PersonalMessages);
            }
        }

        private void ManageMessage(int indexOfSelectedMessage, List<PersonalMessage> PersonalMessages)
        {
            string messageAction = SelectionMenu.Horizontal(new List<string> { "Reply", "Delete", "Back" }, PersonalMessages[indexOfSelectedMessage].MessageText).NameOfChoice;
            switch (messageAction)
            {
                case "Reply":
                    ReplyToMessage(indexOfSelectedMessage, PersonalMessages);
                    return;
                case "Delete":
                    DeleteMessage(IsSender: false);
                    return;
                case "Back":
                    return;
            }
        }

        private List<string> PresentPersonalMessages(List<PersonalMessage> ListOfPersonalMessages)
        {
            return ListOfPersonalMessages
                .Select(pm => (pm.IsMessageRead) ? pm.MessageTitle : pm.MessageTitle + "*")
                .ToList();
        }

        private void ReplyToMessage(int indexOfSelectedMessage, List<PersonalMessage> PersonalMessages)
        {
            MessageContent mc = GetMessageContent();

            PersonalMessage message = new PersonalMessage()
            {
                SenderID = PersonalMessages[indexOfSelectedMessage].RecieverID,
                RecieverID = PersonalMessages[indexOfSelectedMessage].SenderID,
                MessageText = mc.Body,
                MessageTitle = mc.Title
            };
            DataProvider.CreatePersonalMessage(message);
        }

        public void SendPersonalMessage()
        {
            Console.Clear();
            int recieverid = GetRecieverId();
            if (recieverid != -1)
            {
                MessageContent mc = GetMessageContent();
                PersonalMessage message = new PersonalMessage()
                {
                    SenderID = ActiveUser.UserId,
                    RecieverID = recieverid,
                    MessageText = mc.Body,
                    MessageTitle = mc.Title
                };
                DataProvider.CreatePersonalMessage(message);
                Console.Clear();
                Console.WriteLine("Message sent!");
                Console.ReadKey();
            }
        }

        private int GetRecieverId()
        {
            List<string> ListOfRecievers = DataProvider.ReadUsers().Select(u => u.Username).ToList();
            ListOfRecievers.Remove(ActiveUser.Username);
            if (ListOfRecievers.Count == 0)
            {
                return -1;
            }
            else
            {
                string reciever = SelectionMenu.Vertical(ListOfRecievers, "Whom do you want to send your message to?").NameOfChoice;
                Console.CursorVisible = false;
                User userReciever = DataProvider.ReadUsers().SingleOrDefault(u => u.Username == reciever);
                return userReciever.UserId;
            }
        }

        public void ShowSent(bool IsUserSender)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages(ActiveUser, IsUserSender);
            List<string> sentMessages = new List<string>();
            foreach (PersonalMessage message in messageList)
            {
                if (message.IsMessageShownToSender)
                {
                    sentMessages.Add(message.MessageText);
                }
            }
            if (sentMessages.Count == 0)
            {
                Console.WriteLine("No sent messages!");
            }
            else
            {
                for (int i = 0; i < sentMessages.Count; i++)
                {
                    Console.WriteLine(sentMessages[i]);
                }
            }
            Console.ReadKey();
            return;
        }

        public List<PersonalMessage> GetRecievedMessages(bool IsUserSender)
        {
            return DataProvider.ReadPersonalMessages(ActiveUser, IsUserSender)
                .OrderBy(pm => pm.IsMessageRead)
                .ThenBy(pm => pm.DateCreated)
                .ToList();
        }

        public void DeleteMessage(bool IsSender)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages(ActiveUser, IsSender);
            List<string> MessageTextList = new List<string>();
            foreach (PersonalMessage message in messageList)
            {
                if ((message.SenderID == ActiveUser.UserId && message.IsMessageShownToSender) || (message.RecieverID == ActiveUser.UserId && message.IsMessageShownToReciever))
                {
                    MessageTextList.Add(message.MessageText);
                }
            }
            if (MessageTextList.Count == 0)
            {
                Console.WriteLine("No message found!");
                Console.ReadKey();
                return;
            }
            else
            {
                bool IsUserSender;
                MessageTextList.Add("Back");
                int index = SelectionMenu.Vertical(MessageTextList, "Delete any message").IndexOfChoice;
                if (index == MessageTextList.Count - 1)
                {
                    return;
                }
                else
                {
                    if (messageList[index].SenderID == ActiveUser.UserId)
                        IsUserSender = true;
                    else
                        IsUserSender = false;
                    DataProvider.DeleteSelectedPersonalMessage(messageList[index], IsUserSender);
                }
            }
        }
    }
}
