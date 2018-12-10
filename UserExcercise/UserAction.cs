using Excercise1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                string UserToBeUpdated = MainMenuSelection.VerticalMenu(ListOfPossibleUsers);
                User selecteduser = DataProvider.ReadUsers().Single(us => us.Username == UserToBeUpdated);
                List<User> ListOfAllUsers = DataProvider.ReadUsers();
                if (UserToBeUpdated == "Back")
                {
                    return;
                }
                else
                {
                    string selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "guest", "user", "admin" }, $"Choose the privileges of {selecteduser.Username}");
                    switch (selecteditem)
                    {
                        case "guest":
                            DataProvider.UpdateUserAccess(selecteduser, Privilege.guest);
                            break;
                        case "user":
                            DataProvider.UpdateUserAccess(selecteduser, Privilege.user);
                            break;
                        case "admin":
                            DataProvider.UpdateUserAccess(selecteduser, Privilege.admin);
                            break;
                    }
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
                string userToBeDeleted = MainMenuSelection.VerticalMenu(usersList);
                if (userToBeDeleted == "Back")
                {
                    return;
                }
                else
                {
                    DataProvider.DeleteSelectedUser(userToBeDeleted);
                }
                Console.CursorVisible = false;
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
                    string selecteditem = MainMenuSelection.HorizontalMainMenu(new List<string> { "Back", "Exit" }, "Message sent!");
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
                List<string> ListOfRecievers = DataProvider.ReadUsers().Select(u=>u.Username).ToList();
                ListOfRecievers.Remove(sender.Username);
                if (IsListEmpty(ListOfRecievers))
                {
                    return;
                }
                else
                {
                    int senderid , recieverid ;
                    string reciever = MainMenuSelection.VerticalMenu(ListOfRecievers, "Whom do you want to send your message to?");
                    Console.CursorVisible = false;
                    User userReciever = DataProvider.ReadUsers().SingleOrDefault(u=>u.Username==reciever);  //varaei otan apo melina stelnw sthn allh melina gi auto prepei me ta id na tous vriskw//Update to ekana melina2 kai ola kala
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
                    senders.Add(message.RecieverID);
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

        public void DeleteMessage(int userid)
        {
            List<PersonalMessage> messageList = DataProvider.ReadPersonalMessages();
            List<string> MessageTextList = new List<string>();
            foreach (PersonalMessage message in messageList)
            {
                if (message.SenderID == userid || message.RecieverID == userid)
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
                MessageTextList.Add("Back");
                string selecteditem = MainMenuSelection.VerticalMenu(MessageTextList, "Delete any message");
                if (selecteditem == "Back")
                {
                    return;
                }
                else
                {
                    List<string> ListOfMessages = new List<string>(File.ReadAllLines(@"C:\Users\user\Desktop\Personal Messages.txt"));
                    int userposition = 0;
                    int line = -1;
                    foreach (string personalMessage in ListOfMessages)
                    {
                        string[] messagePart = personalMessage.Split(',');
                        if (messagePart[3] == selecteditem)
                        {
                            line = userposition;
                            break;
                        }
                        userposition++;
                    }
                    ListOfMessages.RemoveAt(line);
                    File.WriteAllLines(@"C:\Users\user\Desktop\Personal Messages.txt", ListOfMessages);
                    Console.Write($"User {selecteditem} has been deleted succesfully!");
                    Console.ReadKey();
                }
            }
        }
        
    }
}
