using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Excercise1
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

        public List<PersonalMessage> ReadPersonalMessages()
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return Db.PersonalMessages.ToList();
            }
        }

        public List<User> ReadUsers()
        {
            using (DataBaseClass Db = new DataBaseClass())
            {
                return Db.Users.ToList();
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

        public bool DeleteSelectedUser(string UserToDelete)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSelectedPersonalMessage()
        {
            throw new NotImplementedException();
        }

        public bool DeleteSelectedForumMessage()
        {
            throw new NotImplementedException();
        }
    }
}
