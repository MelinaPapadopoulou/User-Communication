﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Excercise1
{
    class UserAction
    {
        const int MaxCharacters = 250;
        IProvideData DataProvider;
        MenuSelection MainMenuSelection;

        public UserAction(IProvideData dataprovider)
        {
            DataProvider = dataprovider;
            MainMenuSelection = new MenuSelection();
        }

        public void ShowDetails(User ActiveUser)
        {
            Console.Clear();
            Console.WriteLine("Here it is!");
            Console.WriteLine(ActiveUser.Username + " " + ActiveUser.Password + " " + ActiveUser.UsersPrivilege.ToString("g"));
            Console.ReadKey();
            return;
        }

        public void UpdateUser(string admin)
        {
            List<string> ListOfPossibleUsers = DataProvider.ReadUsers().Select(u => u.Username).ToList();
            ListOfPossibleUsers.Remove(admin);
            if (IsListEmpty(ListOfPossibleUsers))
            {
                return;
            }
            else
            {
                ListOfPossibleUsers.Add("Back");
                string UserToBeUpdated = MainMenuSelection.VerticalMenu(ListOfPossibleUsers).NameOfChoice;
                User selecteduser = DataProvider.ReadUsers().Single(us => us.Username == UserToBeUpdated);
                List<User> ListOfAllUsers = DataProvider.ReadUsers();
                if (UserToBeUpdated == "Back")
                {
                    return;
                }
                else
                {
                    Privilege NewUserPrivilege = (Privilege)MainMenuSelection.VerticalMenu(new List<string> { "admin", "user" , "guest"}, $"Choose the privileges of {selecteduser.Username}").IndexOfChoice;
                    DataProvider.UpdateUserAccess(selecteduser, NewUserPrivilege);
                }
            }
        }

        public void CreateUser()
        {
            User u2 = new User();
            Console.WriteLine($"User \"{u2.Username}\" has been created!");
        }

        public void DeleteUser(string admin)
        {
            List<string> usersList = DataProvider.ReadUsers().Select(u => u.Username).ToList();
            usersList.Remove(admin);
            if (IsListEmpty(usersList))
            {
                return;
            }
            else
            {
                usersList.Add("Back");
                string userToDelete = MainMenuSelection.VerticalMenu(usersList).NameOfChoice;
                if (userToDelete == "Back")
                {
                    return;
                }
                else
                {
                    User selecteduser = DataProvider.ReadUsers().Single(us => us.Username == userToDelete);
                    DataProvider.DeleteSelectedUser(selecteduser);
                }
                
            }
        }
        public void ForumMessage(User sender)
        {
            while (true)
            {
                Console.Clear();
                string messageText = WriteMessage();
                ForumMessage message = new ForumMessage()
                {
                    SenderID = sender.UserId,
                    MessageText = messageText
                };
                DataProvider.CreateForumMessage(message);
                while (true)
                {
                    string selecteditem = MainMenuSelection.HorizontalMainMenu(new List<string> { "Back", "Exit" }, "Message sent!").NameOfChoice;
                    switch (selecteditem)
                    {
                        case "Back":
                            return;
                        case "Exit":
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        public void PersonalMessage(User sender)
        {
            while (true)
            {
                Console.Clear();
                List<string> ListOfRecievers = DataProvider.ReadUsers().Select(u => u.Username).ToList();
                ListOfRecievers.Remove(sender.Username);
                if (IsListEmpty(ListOfRecievers))
                {
                    return;
                }
                else
                {
                    int senderid, recieverid;
                    string reciever = MainMenuSelection.VerticalMenu(ListOfRecievers, "Whom do you want to send your message to?").NameOfChoice;
                    Console.CursorVisible = false;
                    User userReciever = DataProvider.ReadUsers().SingleOrDefault(u => u.Username == reciever);  //varaei otan apo melina stelnw sthn allh melina gi auto prepei me ta id na tous vriskw//Update to ekana melina2 kai ola kala
                    senderid = sender.UserId;
                    recieverid = userReciever.UserId;
                    string messageText = WriteMessage();
                    PersonalMessage message = new PersonalMessage()
                    {
                        SenderID = senderid,
                        RecieverID = recieverid,
                        MessageText = messageText
                    };
                    DataProvider.CreatePersonalMessage(message);
                    Console.Clear();
                    Console.WriteLine("Message sent!");
                    Console.ReadKey();
                    return;
                }
            }
        }

        private bool IsListEmpty(List<string> list)     //Used for users and messages
        {
            if (list.Count == 0)
            {
                return true;
            }
            return false;
        }

        public string WriteMessage()
        {
            ConsoleKeyInfo messagekey;
            int remainingCharacters = 250;
            string messageText = "";
            Console.CursorVisible = false;
            Console.WriteLine("Send your message!");
            do
            {
                messagekey = Console.ReadKey(true);
                if (messagekey.Key != ConsoleKey.Backspace && messagekey.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    messageText += messagekey.KeyChar;
                    remainingCharacters--;
                    Console.Write(messageText + $"\n\n{remainingCharacters}/{MaxCharacters}");
                    Console.SetCursorPosition(messageText.Length, 0);
                }
                else if (messagekey.Key == ConsoleKey.Backspace && messageText.Length > 0)
                {
                    messageText = messageText.Remove(messageText.Length - 1);
                    remainingCharacters++;
                    Console.Clear();
                    Console.Write(messageText + $"\n\n{remainingCharacters}/{MaxCharacters}");
                }
            } while (remainingCharacters > 0 && messagekey.Key != ConsoleKey.Enter);
            return messageText;
        }

        public void ShowSent(int userid)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages();
            List<string> sentMessages = new List<string>();
            foreach (PersonalMessage message in messageList)
            {
                if (message.SenderID == userid)
                {
                    Debug.WriteLine("Mpikame!!!");
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

        public void ShowRecieved(int userid)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages();
            List<string> recievedMessages = new List<string>();
            List<int> senders = new List<int>();
            foreach (PersonalMessage message in messageList)
            {
                if (message.RecieverID == userid)
                {
                    recievedMessages.Add(message.MessageText);
                }
            }
            if (recievedMessages.Count == 0)
            {
                Console.WriteLine("No recieved messages!");
            }
            else
            {
                for (int i = 0; i < recievedMessages.Count; i++)
                {
                    Console.WriteLine(recievedMessages[i]);
                }
            }
            Console.ReadKey();
            return;
        }

        public void DeleteMessage(User ActiveUser)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages();
            List<string> MessageTextList = new List<string>();
            foreach (PersonalMessage message in messageList)
            {
                if ((message.SenderID == ActiveUser.UserId && message.IsMessageShownToSender) || (message.RecieverID == ActiveUser.UserId && message.IsMessageShownToReciever))
                {
                    MessageTextList.Add(message.MessageText);
                }
            }
            if (IsListEmpty(MessageTextList))
            {
                Console.WriteLine("No message found!");
                Console.ReadKey();
                return;
            }
            else
            {
                bool IsUserSender;
                MessageTextList.Add("Back");
                int index = MainMenuSelection.VerticalMenu(MessageTextList, "Delete any message").IndexOfChoice;
                if (index == MessageTextList.Count - 1)
                {
                    return;
                }
                else
                {
                    // Gia na apofugw thn foreach na kanw to menu na stelnei int kai oxi string
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
