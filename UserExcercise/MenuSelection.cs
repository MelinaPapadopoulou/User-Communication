using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise1
{
    class MenuSelection
    {
        public string HorizontalMainMenu(List<string> list, string Header = "")
        {
            int index = 0;
            ConsoleKeyInfo ckey;
            Console.CursorVisible = false;
            do
            {

                Console.Clear();
                Console.WriteLine(Header);
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(list[i] + "\t");
                    }
                    else
                    {
                        Console.Write(list[i] + "\t");
                    }
                    Console.ResetColor();
                }
                ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.RightArrow)
                {
                    if (index == list.Count - 1)
                    {
                        //index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                else if (ckey.Key == ConsoleKey.LeftArrow)
                {
                    if (index <= 0)
                    {
                        //index = list.Count - 1;
                    }
                    else
                    {
                        index--;
                    }

                }
                Console.Clear();
            } while (ckey.Key != ConsoleKey.Enter);
            return list[index];
        }
        public string VerticalMenu(List<string> list, string Header = "")
        {

            Console.CursorVisible = false;
            int index = 0;
            ConsoleKeyInfo ckey;
            do
            {

                Console.Clear();
                Console.WriteLine(Header);
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(list[i]);
                    }
                    else
                    {
                        Console.WriteLine(list[i]);
                    }
                    Console.ResetColor();
                }
                ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index == list.Count - 1)
                    {
                        //index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        //index = list.Count - 1;
                    }
                    else
                    {
                        index--;
                    }

                }
                Console.Clear();
            } while (ckey.Key != ConsoleKey.Enter);
             return list[index];
        }
    }
}
