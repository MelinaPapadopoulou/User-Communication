using System.Collections.Generic;

namespace UserExcercise
{
    internal interface IProvideData
    {
        bool IsStorageEmpty();

        // Create
        bool CreateUser(User user);
        bool CreatePersonalMessage(PersonalMessage personalMessage);
        bool CreateForumMessage(ForumMessage forumMessage);

        // Read
        List<User> ReadUsers();
        List<PersonalMessage> ReadPersonalMessages(User ActiveUser,bool IsUserSender);
        List<ForumMessage> ReadForumMessages();
        List<ForumMessage> GetLastForumMessages(int NumberOfMessages = 20);
        
        

        // Update
        bool UpdateUserName(User UserToUpdate,string NewUsername);
        bool UpdateUserPassword(User UserToUpdate,string NewPassword);
        bool UpdateUserAccess(User UserToUpdate,Privilege NewPrivilege);
        bool UpdateMessageAsRead(int pm);

        // Delete
        bool DeleteSelectedUser(User UserToDelete);
        bool DeleteSelectedPersonalMessage(PersonalMessage personalMessage,bool IsUserSender);
        bool DeleteSelectedForumMessage();

    }
}