using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UserExcercise
{
    class UserManager
    {
        IProvideData DataProvider;
        SelectionMenu SelectionMenu;
        User ActiveUser;

        public UserManager(IProvideData dataprovider,User activeuser)
        {
            DataProvider = dataprovider;
            SelectionMenu = new SelectionMenu();
            ActiveUser = activeuser;
        }

        public void ShowDetails()
        {
            Console.Clear();
            Console.WriteLine("Details");
            Console.WriteLine("Username : " + ActiveUser.Username + Environment.NewLine + "Password : " + ActiveUser.Password + Environment.NewLine + "As : " + ActiveUser.UsersPrivilege.ToString("g"));
            Console.ReadKey();
            return;
        }

        public void UpdateUser()
        {
            List<string> ListOfPossibleUsers = DataProvider.ReadUsers().Select(u => u.Username).ToList();
            ListOfPossibleUsers.Remove(ActiveUser.Username);
            if (ListOfPossibleUsers.Count==0)
            {
                Console.WriteLine("No user found");
                Console.ReadKey();
                return;
            }
            else
            {
                ListOfPossibleUsers.Add("Back");
                string UserToBeUpdated = SelectionMenu.Vertical(ListOfPossibleUsers).NameOfChoice;
                List<User> ListOfAllUsers = DataProvider.ReadUsers();
                if (UserToBeUpdated == "Back")
                {
                    return;
                }
                else
                {
                    User selecteduser = DataProvider.ReadUsers().Single(us => us.Username == UserToBeUpdated);
                    Privilege NewUserPrivilege = (Privilege)SelectionMenu.Vertical(new List<string> { "admin", "user", "guest" }, $"Choose the privileges of {selecteduser.Username}").IndexOfChoice;
                    DataProvider.UpdateUserAccess(selecteduser, NewUserPrivilege);
                }
            }
        }

        public void CreateUser()
        {
            LoginOrSignup CreateNewUser = new LoginOrSignup(DataProvider);
            User u2 = new User()
            {
                Username = CreateNewUser.ReadUsername(),
                Password = CreateNewUser.ReadPassword(),
                UsersPrivilege = Privilege.user
            };
            DataProvider.CreateUser(u2);
            Console.WriteLine($"User \"{u2.Username}\" has been created!");
        }

        public void Disable()
        {
            List<string> usersList = DataProvider.ReadUsers().Where(u=>u.UserId!= ActiveUser.UserId).Select(u => u.Username).ToList();
            if (usersList.Count==0)
            {
                Console.WriteLine("No user found");
                Console.ReadKey();
                return;
            }
            else
            {
                usersList.Add("Back");
                string userToDelete = SelectionMenu.Vertical(usersList).NameOfChoice;
                if (userToDelete == "Back")
                {
                    return;
                }
                else
                {
                    User selecteduser = DataProvider.ReadUsers().Single(us => us.Username == userToDelete);
                    DataProvider.DeleteSelectedUser(selecteduser);
                }

            }
        }
    }
}
