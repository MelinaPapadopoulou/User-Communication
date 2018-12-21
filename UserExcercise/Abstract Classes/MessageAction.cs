using System;

namespace UserExcercise
{
    class MessageAction:Action
    {
        protected UserManager MainActions;

        public MessageAction(IProvideData dataprovider, User activeuser):base(activeuser,dataprovider)
        {
            MainActions = new UserManager(DataProvider, ActiveUser);
        }


        protected MessageContent GetMessageContent()
        {
            return new MessageContent()
            {
                Title = WriteMessage("Title:", 25),
                Body = WriteMessage("Body:", 250)
            };
        }

        protected string WriteMessage(string Input, int MaxChars)
        {
            ConsoleKeyInfo messagekey;
            int remainingCharacters = MaxChars;
            string messageText = "";
            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                Console.WriteLine(Input);
                do
                {
                    messagekey = Console.ReadKey(true);
                    if (messagekey.Key != ConsoleKey.Backspace && messagekey.Key != ConsoleKey.Enter)
                    {
                        Console.Clear();
                        messageText += messagekey.KeyChar;
                        remainingCharacters--;
                        Console.WriteLine(Input);
                        Console.Write(messageText + $"\n\n{remainingCharacters}/{MaxChars}");
                        Console.SetCursorPosition(messageText.Length, 0);

                    }
                    else if (messagekey.Key == ConsoleKey.Backspace && messageText.Length > 0)
                    {
                        messageText = messageText.Remove(messageText.Length - 1);
                        remainingCharacters++;
                        Console.Clear();
                        Console.WriteLine(Input);
                        Console.Write(messageText + $"\n\n{remainingCharacters}/{MaxChars}\n");
                    }
                } while (remainingCharacters > 0 && messagekey.Key != ConsoleKey.Enter);
                if (String.IsNullOrWhiteSpace(messageText))
                {
                    messageText = "";
                    remainingCharacters = MaxChars;
                    Console.WriteLine("\nNo empty messages permitted!\nWrite down your message: ");
                }
            } while (String.IsNullOrWhiteSpace(messageText));
            return messageText;
        }
    }
}
