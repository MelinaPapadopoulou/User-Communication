using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace Excercise1
{
    class Program
    {

        static void Main(string[] args)
        {
            IProvideData DataReader = new ReadDataFromFile();
            LoginOrSignup LogOrSign = new LoginOrSignup(DataReader);

            while (true)
            {
                User ActiveUser = LogOrSign.LoginSignup(DataReader);
                MainMenu ConsoleMainMenu = new MainMenu(DataReader);
                ConsoleMainMenu.ShowMenu(ActiveUser);
            }
        }
    }
}

