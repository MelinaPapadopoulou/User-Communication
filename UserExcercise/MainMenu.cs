using Excercise1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader);
                            break;
                        case Privilege.user:
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Show Details", "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader);
                            break;
                        case Privilege.admin:
                            selecteditem = MainMenuSelection.VerticalMenu(new List<string> { "Update User", "Delete User", "Create User", "Show Details", "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader);
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
                            MainActions.CreateUser();
                            break;
                        case "Delete User":
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
                messageselection = MainMenuSelection.VerticalMenu(new List<string> { "Send Message", "Sent", "Recieved", "Delete Message", "Back" }, Headers.MessageMenuHeader);
                switch (messageselection)
                {
                    case "Send Message":
                        MainActions.PersonalMessage(ActiveUser);
                        return;
                    case "Sent":
                        MainActions.ShowSent(ActiveUser.UserId);
                        return;
                    case "Recieved":
                        MainActions.ShowRecieved(ActiveUser.UserId);
                        return;
                    case "Delete Message":
                        MainActions.DeleteMessage(ActiveUser.UserId);
                        return;
                    case "Back":
                        return;
                }
            }
        }
    }
}
