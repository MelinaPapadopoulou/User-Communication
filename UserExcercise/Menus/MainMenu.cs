using System;
using System.Collections.Generic;
using System.Linq;

namespace UserExcercise
{
    class MainMenu: IDisposable
    {
        IProvideData DataProvider;
        SelectionMenu SelectionMenu;
        SpecificMenu SpecificMenuActions;
        User ActiveUser;

        public MainMenu(IProvideData dataprovider, User LoggedInUser)
        {
            ActiveUser = LoggedInUser;
            DataProvider = dataprovider;
            SelectionMenu = new SelectionMenu();
            SpecificMenuActions = new SpecificMenu(DataProvider, ActiveUser);
        }

        public void Dispose()
        {
        }

        public void ShowMenu()
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
                            selecteditem = SelectionMenu.Vertical(new List<string> { "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                        case Privilege.user:
                            selecteditem = SelectionMenu.Vertical(new List<string> { "Manage User", "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                        case Privilege.admin:
                            selecteditem = SelectionMenu.Vertical(new List<string> { "Manage User",  "Our Space", "Messages", "Log Out", "Exit" }, Headers.MainMenuHeader).NameOfChoice;
                            break;
                    }
                    switch (selecteditem)
                    {
                        case "Our Space":
                            SpecificMenuActions.Forum();
                            break;
                        case "Messages":
                            SpecificMenuActions.MessagesMenu(ActiveUser);
                            break;
                        case "Exit":
                            Environment.Exit(0);
                            break;
                        case "Log Out":
                            return;
                        case "Manage User":
                            SpecificMenuActions.ManageUserMenu();
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
    }
}
