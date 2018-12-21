using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UserExcercise
{
    class LoginOrSignup
    {
        IProvideData DataProvider;
        SelectionMenu SelectionMenu;

        public LoginOrSignup(IProvideData dataprovider)
        {
            SelectionMenu = new SelectionMenu();
            DataProvider = dataprovider;
        }

        public User LoginSignup(IProvideData DataProvider,string startingOption= "SignUp")
        {
            
            startingOption = SelectionMenu.Horizontal(new List<string> { "Login", "SignUp", "Exit" }, Headers.headerw).NameOfChoice;
            Console.Clear();
            if (startingOption == "Exit")
            {
                Environment.Exit(0);
            }

            string GivenUsername = ReadUsername();
            string GivenPassword = ReadPassword();

            if (startingOption == "SignUp")
            {
                User u1 = new User()
                {
                    Username = GivenUsername,
                    Password = GivenPassword,
                    UsersPrivilege = DataProvider.IsStorageEmpty() ? Privilege.admin : Privilege.user
                };

                Debug.Write("New User was created successfully: " + DataProvider.CreateUser(u1));
                return u1;
            }
            else if (startingOption == "Login")
            {
                return ValidLogin(GivenUsername, GivenPassword);
            }
            return null;
        }

        public User ValidLogin( string username, string password)
        {
            List<User> currentlist = DataProvider.ReadUsers();
            foreach (User user in currentlist)
            {
                if (username == user.Username && password == user.Password)
                {
                    return user;
                }
            }
            return null;
        }

        public string ReadPassword()
        {
            Console.Write("Password: ");
            Console.CursorVisible = true;
            string password = "";
            while (true)
            {
                ConsoleKeyInfo passkey;
                do
                {
                    passkey = Console.ReadKey(true);
                    if (passkey.Key != ConsoleKey.Backspace && passkey.Key != ConsoleKey.Enter)
                    {
                        password += passkey.KeyChar;
                        Console.Write("*");
                    }
                    else if (passkey.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                    }
                } while (passkey.Key != ConsoleKey.Enter);
                if (PasswordIncorect(password))
                {
                    Console.Write("Password must contain both letters and numbers. Try again!\nPassword: ");
                }
                else
                {
                    return password;
                }
            }
        }
        private bool PasswordIncorect(string password)
        {
            return password.All(char.IsLetter) || password.All(char.IsNumber);
        }

        public string ReadUsername()
        {
            Console.Write("Username: ");
            Console.CursorVisible = true;
            string userName;
            while (true)
            {
                userName = Console.ReadLine();
                if (!IsInputCorrect(userName))
                {
                    Console.Write("Wrong input. Try again!\nUsername: ");

                }
                else if (!IsInputLetter(userName))
                {
                    Console.Write("Your username contains numbers. Try again!\nUsername: ");
                }
                else
                {
                    return userName;
                }
            }
        }

        private bool IsInputCorrect(string userName)
        {
            return (userName.Length > 5 && userName.Length < 20);
        }

        private bool IsInputLetter(string userName)
        {
            return userName.All(char.IsLetter);
        }
    }
}
