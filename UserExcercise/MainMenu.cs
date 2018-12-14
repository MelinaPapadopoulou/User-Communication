using System;
using System.Collections.Generic;

namespace Excercise1
{
    class MainMenu
    {
        IProvideData DataProvider;
        MenuSelection MainMenuSelection;
        UserAction MainActions;
        public MainMenu(IProvideData dataprovider)
        {
            DataProvider = dataprovider;
            MainMenuSelection = new MenuSelection();
            MainActions = new UserAction(DataProvider);
        }
        public void ShowMenu(User ActiveUser)
        {

            if (ActiveUser != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    string selecteditem = "";

                    switch (ActiveUser.UsersPrivilege)
                    {

                        case Privilege.guest:
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                        case Privilege.user:
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Show Details", "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                        case Privilege.admin:
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Update User", "Disable User", "Create User", "Show Details", "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                    }
                    switch (selecteditem)
                    {
                        case "Our Space":
                            MainActions.ForumMessage(ActiveUser);
                            break;
                        case "Messages":
                            MessagesMenu(ActiveUser);
                            break;
                        case "Exit":
                            Environment.Exit(0);
                            break;
                        case "Log Out":
                            return;
                        case "Show Details":
                            MainActions.ShowDetails(ActiveUser);
                            break;
                        case "Create User":
                            MainActions.CreateUser(DataProvider);
                            break;
                        case "Disable User":
                            MainActions.DeleteUser(ActiveUser.Username);
                            break; ;
                        case "Update User":
                            MainActions.UpdateUser(ActiveUser.Username);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nInvalid! Sorry! No Access!");
                Console.ReadKey();
            }
        }
        public void MessagesMenu(User ActiveUser)
        {
            string messageselection;
            while (true)
            {
                messageselection = MainMenuSelection.VerticalMenu(new List<string> { "Send Message", "Sent", "Recieved", "Delete Sent Message", "Delete Recieved Message", "Back" }, Headers.MessageMenuHeader).NameOfChoice;
                switch (messageselection)
                {
                    case "Send Message":
                        MainActions.PersonalMessage(ActiveUser);
                        break;
                    case "Sent":
                        MainActions.ShowSent(ActiveUser,IsUserSender:true);
                        break;
                    case "Recieved":
                        MainActions.ShowRecieved(ActiveUser,IsUserSender:false);
                        break;
                    case "Delete Sent Message":
                        MainActions.DeleteMessage(ActiveUser, IsSender: true);
                        break;
                    case "Delete Recieved Message":
                        MainActions.DeleteMessage(ActiveUser, IsSender: false);
                        break;
                    case "Back":
                        return;
                }
            }
        }
    }
}
