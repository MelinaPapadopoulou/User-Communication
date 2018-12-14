using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Excercise1
{
    class ReadDataFromFile : IProvideData
    {
        private string PATH = @"C:\Users\user\Desktop\";
        const string USERNAME = "Usernames.txt", PERSONAL_MESSAGES = "Personal Messages.txt", FORUM_MESSAGES = "Forum Messages.txt";

        public List<User> ReadUsers()
        {
            string[] lines = File.ReadAllLines(PATH + USERNAME);
            List<User> ListOfUsers = new List<User>();
            foreach (string line in lines)
            {
                string[] linesPart = line.Split(',');
                ListOfUsers.Add(new User()
                {
                    Username = linesPart[0],
                    Password = linesPart[1],
                    UsersPrivilege = (Privilege)int.Parse(linesPart[2]),
                    UserId = int.Parse(linesPart[3])
                });
            }
            return ListOfUsers;
        }

        public List<PersonalMessage> ReadPersonalMessages(User ActiveUser, bool IsUserSender)
        {
            return File.ReadAllLines(PATH + PERSONAL_MESSAGES)
                    .Where(l => int.Parse(l.Split(',')[IsUserSender ? 1 : 2]) == ActiveUser.UserId)
                    .Select(l => TextToPersonalMessage(l))
                    .ToList();
        }
        private PersonalMessage TextToPersonalMessage(string Line)
        {
            string[] Text = Line.Split(',');
            return new PersonalMessage
            {
                DateCreated = DateTime.Parse(Text[0]),
                SenderID = int.Parse(Text[1]),
                RecieverID = int.Parse(Text[2]),
                MessageText = Text[3],
                PersonalMessageId = int.Parse(Text[4])
            };
        }

        public List<ForumMessage> ReadForumMessages()
        {
            string[] lines = File.ReadAllLines(PATH + FORUM_MESSAGES);
            List<ForumMessage> ListOfMessages = new List<ForumMessage>();
            foreach (string line in lines)
            {
                string[] linesPart = line.Split(',');
                ListOfMessages.Add(new ForumMessage()
                {
                    SenderID = int.Parse(linesPart[1]),
                    MessageText = linesPart[1],
                    ForumMessageID = int.Parse(linesPart[2])
                });
            }
            return ListOfMessages;
        }

        public bool CreateUser(User user)
        {
            string path = PATH + USERNAME;
            return SaveData(path, $"{user.Username},{user.Password},{(int)user.UsersPrivilege},{GetNewUserID()}" + Environment.NewLine);
        }

        public bool CreatePersonalMessage(PersonalMessage personalMessage)
        {
            string messagepath = PATH + PERSONAL_MESSAGES;
            return SaveData(messagepath, DateTime.UtcNow.ToString() + "," + personalMessage.SenderID + ", " + personalMessage.RecieverID + "," + personalMessage.MessageText + "," + GetNewPersonalMessageID() + Environment.NewLine);
        }

        public bool CreateForumMessage(ForumMessage forumMessage)
        {
            string messagepath = PATH + FORUM_MESSAGES;
            return SaveData(messagepath, DateTime.UtcNow.ToString() + "," + forumMessage.SenderID + "," + forumMessage.MessageText + "," + GetNewForumMessageID() + Environment.NewLine);

        }
        public bool SaveData(string path, string dataToSave)
        {
            try
            {
                File.AppendAllText(path, dataToSave);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateUserName(User UserToUpdate, string NewUsername)
        {
            string path = PATH + USERNAME;
            UserToUpdate.Username = NewUsername;
            return CommitChanges(UserToUpdate, path);
        }

        public bool UpdateUserPassword(User UserToUpdate, string NewPassword)
        {
            string path = PATH + USERNAME;
            UserToUpdate.Password = NewPassword;
            return CommitChanges(UserToUpdate, path);
        }
        public bool CommitChanges(User UserToUpdate, string path)
        {
            string[] usersArray = File.ReadAllLines(PATH + USERNAME);
            List<string> userlist = usersArray.OfType<string>().ToList();
            int line = FindLineOfUser(UserToUpdate.UserId);
            userlist[line] = UserToUpdate.Username + "," + UserToUpdate.Password + "," + UserToUpdate.UsersPrivilege + "," + UserToUpdate.UserId + Environment.NewLine;
            File.WriteAllLines(path, userlist);
            return true;
        }
        public bool UpdateUserAccess(User UserToUpdate, Privilege NewPrivilege)
        {
            string path = PATH + USERNAME;
            UserToUpdate.UsersPrivilege = NewPrivilege;
            return CommitChanges(UserToUpdate, path);
        }
        public int FindLineOfUser(int userid)
        {
            List<string> usersList = new List<string>(File.ReadAllLines(PATH + USERNAME));
            int line = 0;
            List<User> tobeupdated = ReadUsers();
            foreach (User user in tobeupdated)
            {
                if (user.UserId == userid)
                {
                    return line;
                }
                line++;
            }
            return -1;
        }

        private int GetNewUserID()
        {
            try
            {
                return ReadUsers().Select(u => u.UserId).Max() + 1;
            }
            catch (Exception e)
            {
                return 1;
            }
        }

        private int GetNewPersonalMessageID()
        {
            try
            {
                return File
                    .ReadAllLines(PATH + PERSONAL_MESSAGES)
                    .Select(l => int.Parse(l.Split(',')[4]))
                    .Max() + 1;
            }
            catch (Exception e)
            {
                return 1;
            }
        }
        private int GetNewForumMessageID()
        {
            try
            {
                return ReadForumMessages().Select(fm => fm.ForumMessageID).Max() + 1;
            }
            catch (Exception e)
            {
                return 1;
            }
        }

        public bool DeleteSelectedUser(User UserToDelete)
        {
            List<string> usersList = new List<string>(File.ReadAllLines(PATH + USERNAME));
            int line = FindLineOfUser(UserToDelete.UserId);
            usersList.RemoveAt(line);
            File.WriteAllLines(PATH + USERNAME, usersList);
            Console.WriteLine($"User has been deleted succesfully!");
            return true;
        }

        public bool DeleteSelectedPersonalMessage(PersonalMessage personalmessage, bool IsUserSender)
        {
            if (IsUserSender && !personalmessage.IsMessageShownToReciever || !IsUserSender && personalmessage.IsMessageShownToSender)
            {
                List<string> ListOfMessages = new List<string>(File.ReadAllLines(PATH + PERSONAL_MESSAGES));
                int messageposition = 0;
                int line = -1;
                foreach (string personalMessage in ListOfMessages)
                {
                    string[] messagePart = personalMessage.Split(',');
                    if (messagePart[3] == personalmessage.MessageText)
                    {
                        line = messageposition;
                        break;
                    }
                    messageposition++;
                }
                if (line != -1)
                {
                    ListOfMessages.RemoveAt(line);
                    File.WriteAllLines(PATH + PERSONAL_MESSAGES, ListOfMessages);
                    return true;
                }
            }
            else
            {
                if (!IsUserSender)
                    personalmessage.IsMessageShownToReciever = false;
                else
                    personalmessage.IsMessageShownToSender = false;
            }
            return false;
        }

        public bool DeleteSelectedForumMessage()
        {
            throw new NotImplementedException();
        }

        public bool IsStorageEmpty()
        {
            return File.Exists(PATH + USERNAME) && !(new FileInfo(PATH + USERNAME).Length > 0);
        }
    }
}
