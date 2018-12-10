﻿using System.Collections.Generic;

namespace Excercise1
{
    internal interface IProvideData
    {
        // Create
        bool CreateUser(User user);
        bool CreatePersonalMessage(PersonalMessage personalMessage);
        bool CreateForumMessage(ForumMessage forumMessage);

        // Read
        List<User> ReadUsers();
        List<PersonalMessage> ReadPersonalMessages();
        List<ForumMessage> ReadForumMessages();

        // Update
        bool UpdateUserName(User UserToUpdate,string NewUsername);
        bool UpdateUserPassword(User UserToUpdate,string NewPassword);
        bool UpdateUserAccess(User UserToUpdate,Privilege NewPrivilege);

        // Delete
        bool DeleteSelectedUser(string UserToDelete);
        bool DeleteSelectedPersonalMessage();
        bool DeleteSelectedForumMessage();

    }
}