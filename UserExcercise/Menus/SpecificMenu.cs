using System.Collections.Generic;
using System;
namespace UserExcercise
{
    class SpecificMenu
    {
        const string REPLY = "Reply", SEND_MESSAGE = "Send Message", DISABLE_USER = "Disable User",CREATE_USER="Create User", 
            UPGRADE_USER = "Upgrade User", SENT = "Sent", DELETE_SENT = "Delete Sent Message", DELETE_RECIEVED = "Delete Recieved Message",
            BACK = "Back",SHOW_DETAILS="Show Details";

        IProvideData DataProvider;
        User ActiveUser;
        UserManager ManageUser;
        PersonalMessageAction PersonalMessageActions;

        ForumMessageAction ForumMessageActions;

        public SpecificMenu(IProvideData dataprovider, User activeuser)
        {
            DataProvider = dataprovider;
            ActiveUser = activeuser;
            ForumMessageActions = new ForumMessageAction(DataProvider, ActiveUser);
            PersonalMessageActions = new PersonalMessageAction(DataProvider,ActiveUser);
            ManageUser = new UserManager(DataProvider, ActiveUser);
        }


        public void MessagesMenu(User ActiveUser)
        {
            string messageselection;
            while (true)
            {
                string RecievedMessages = "Recieved" + PersonalMessageActions.CheckForUnreadMessages();
                messageselection = SelectionMenu.Vertical(new List<string> { SEND_MESSAGE, SENT, RecievedMessages,DELETE_SENT,DELETE_RECIEVED,BACK }, Headers.MessageMenuHeader).NameOfChoice;

                if (messageselection == SEND_MESSAGE)
                    PersonalMessageActions.SendPersonalMessage();
                else if (messageselection == SENT)
                    PersonalMessageActions.ShowSent(IsUserSender: true);
                else if (messageselection == RecievedMessages)
                    PersonalMessageActions.ShowRecievedMessages();
                else if (messageselection == DELETE_SENT)
                    PersonalMessageActions.DeleteMessage(IsSender: true);
                else if (messageselection == DELETE_RECIEVED)
                    PersonalMessageActions.DeleteMessage(IsSender: false);
                else
                    return;
            }
        }


        public void ManageUserMenu()
        {
            string actionselected;
            while (true)
            {
                List<string> Options = (ActiveUser.UsersPrivilege == Privilege.admin) ?
                    new List<string> { DISABLE_USER, UPGRADE_USER, CREATE_USER, SHOW_DETAILS ,BACK}
                    : new List<string> { SHOW_DETAILS,BACK };

                actionselected = SelectionMenu.Vertical(Options).NameOfChoice;
                switch (actionselected)
                {
                    case DISABLE_USER:
                        ManageUser.Disable();
                        break;
                    case UPGRADE_USER:
                        ManageUser.UpdateUser();
                        break;
                    case CREATE_USER:
                        ManageUser.CreateUser();
                        break;
                    case SHOW_DETAILS:
                        ManageUser.ShowDetails();
                        break;
                    case BACK:
                        return;
                }
            }
        }


        public void Forum()
        {
            while (true)
            {
                
                string SelectedAction = SelectionMenu.Horizontal(new List<string> { SEND_MESSAGE, BACK }, ForumMessageActions.ShowPrevious()).NameOfChoice;
                switch (SelectedAction)
                {
                    case SEND_MESSAGE:
                        ForumMessageActions.Send();
                        break;
                    case BACK:
                        return;
                }
            }
            
        }
    }
}
