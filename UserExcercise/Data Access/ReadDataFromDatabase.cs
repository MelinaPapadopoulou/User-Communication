﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;

namespace UserExcercise
{
    class ReadDataFromDatabase : IProvideData
    {
        public List<ForumMessage> ReadForumMessages()
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return Db.ForumMessages.ToList();
            }
        }

        public List<ForumMessage> GetLastForumMessages(int NumberOfMessages)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return Db.ForumMessages
                    .Include(fm => fm.Sender)
                    .OrderByDescending(fm=>fm.ForumMessageID)
                    .Take(NumberOfMessages)
                    .ToList();
            }
        }

        public List<PersonalMessage> ReadPersonalMessages(User ActiveUser, bool IsUserSender)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                if (IsUserSender)
                {
                    return Db.PersonalMessages
                         .Where(s => s.SenderID == ActiveUser.UserId && s.IsMessageShownToSender)
                         .ToList();
                }
                else
                {
                    return Db.PersonalMessages
                        .Where(r => r.RecieverID == ActiveUser.UserId && r.IsMessageShownToReciever)
                        .ToList();
                }
            }
        }


        public List<User> ReadUsers()
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return Db.Users.Where(user=>user.UsersPrivilege!=Privilege.disabled).ToList();
            }
        }

        public bool CreateForumMessage(ForumMessage forumMessage)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                Db.ForumMessages.Add(forumMessage);
                return SaveData(Db);
            }
        }

        public bool SaveData(DataBaseClass Db)
        {
            try
            {
                return Db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Debug.Write("Could not save to database: " + e.StackTrace);
                return false;
            }
        }

        public bool CreatePersonalMessage(PersonalMessage personalMessage)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                Db.PersonalMessages.Add(personalMessage);
                return SaveData(Db);
            }
        }

        public bool CreateUser(User user)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                Db.Users.Add(user);
                return SaveData(Db);
            }
        }

        public bool UpdateUserName(User UserToUpdate, string NewUsername)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                UserToUpdate.Username = NewUsername;
                return SaveData(Db);
            }
        }

        public bool UpdateUserPassword(User UserToUpdate, string NewPassword)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                UserToUpdate.Password = NewPassword;
                return SaveData(Db);
            }
        }

        public bool UpdateUserAccess(User UserToUpdate, Privilege NewPrivilege)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                UserToUpdate.UsersPrivilege = NewPrivilege;
                return SaveData(Db);
            }
        }

        public bool DeleteSelectedUser(User UserToDelete)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                User user = Db.Users.Single(u=>u.UserId == UserToDelete.UserId);
                DeleteAllMessages(user);
                user.Username += "*";
                user.UsersPrivilege = Privilege.disabled;

                
                return SaveData(Db);
            }
        }

        private bool DeleteAllMessages(User UserToDelete)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                User user = Db.Users.SingleOrDefault(u => u.UserId == UserToDelete.UserId);
                List<PersonalMessage>  pmslist = Db.PersonalMessages.Where(pm => pm.SenderID == user.UserId || pm.RecieverID==user.UserId).ToList();
                foreach(PersonalMessage pm in pmslist)
                {
                    if (pm.SenderID==user.UserId)
                    {
                        pm.IsMessageShownToSender = false;
                        DeleteSelectedPersonalMessage(pm, IsUserSender: true);
                    }
                    else
                    {
                        pm.IsMessageShownToReciever = false;
                        DeleteSelectedPersonalMessage(pm, IsUserSender: false);
                    }
                }
                return SaveData(Db);
            }
        }

        public bool DeleteSelectedPersonalMessage(PersonalMessage personalmessage, bool IsUserSender)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                PersonalMessage pm = Db.PersonalMessages.Single(Perm => Perm.PersonalMessageId == personalmessage.PersonalMessageId);
                if (IsUserSender && !personalmessage.IsMessageShownToReciever || !IsUserSender && !personalmessage.IsMessageShownToSender)
                {                    
                    Db.PersonalMessages.Remove(pm);
                }
                else
                {
                    if (!IsUserSender)
                        pm.IsMessageShownToReciever = false;
                    else
                        pm.IsMessageShownToSender = false;
                }
                return SaveData(Db);
            }
        }

        public bool DeleteSelectedForumMessage()
        {
            //No delete option
            throw new Exception();
        }

        public bool IsStorageEmpty()
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return !Db.Users.Any();
            }
        }

        public bool UpdateMessageAsRead(int pmid)
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                Db.PersonalMessages.Single(pm => pm.PersonalMessageId == pmid).IsMessageRead = true;
                return SaveData(Db);
            }
        }
    }
}
