using System;
using System.Threading.Tasks;

namespace SimpleTextEditor
{
    public class UI
    {
        // Write message with delay for better user experience
        public static void WriteMessage(string message, int speedDelay = 50) // Write message with delay
        {
            foreach (var c in message)
            {
                Console.Write(c);
                Task.Delay(speedDelay).Wait();
            }
            Console.WriteLine();
        }

        // Check if user wants to continue
        public static void checkContinue() 
        {
            string continueMessage = "Do you want to continue? (y/n)";
            WriteMessage(continueMessage);

            string isContinue = Console.ReadKey().KeyChar.ToString().ToLower();

            if (isContinue == "y")
            {
                Console.WriteLine();
                Program.Main(null);
            }

            else if (isContinue == "n")
            {
                string exitMessage = "\nExiting...";
                WriteMessage(exitMessage);
            }

            else
            {
                string invalidContinueMessage = "\nInvalid input!";
                WriteMessage(invalidContinueMessage);
                checkContinue();
            }
        }
    }
}