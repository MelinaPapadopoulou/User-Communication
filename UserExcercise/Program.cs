using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace UserExcercise
{
    class Program
    {

        static void Main(string[] args)
        {
            IProvideData DataReader = new ReadDataFromDatabase();
            LoginOrSignup LogOrSign = new LoginOrSignup(DataReader);

            while (true)
            {
                User ActiveUser = LogOrSign.LoginSignup(DataReader);
                using (MainMenu ConsoleMainMenu = new MainMenu(DataReader, ActiveUser))
                {
                    ConsoleMainMenu.ShowMenu();
                }
                    
            }
        }
    }
}

