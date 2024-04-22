using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTextEditor
{
    class Program
    {
        static void WriteMessage(string message, int speedDelay = 50) // Write message with delay
        {
            foreach (var c in message)
            {
                Console.Write(c);
                Task.Delay(speedDelay).Wait();
            }
            Console.WriteLine();
        }

        static void ReadFile(string filePath) // Read file
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Console.WriteLine();
                string wait = "--------------------";
                WriteMessage(wait, 100);

                while (sr.Peek() > -1)
                {
                    Console.WriteLine(sr.ReadLine());
                }

                WriteMessage(wait, 100);   
            }
        }

        static void WriteFile(string filePath) // Write to file
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            if (string.IsNullOrEmpty(directoryPath)) // If directory path is empty, set it to current directory
            {
                directoryPath = Directory.GetCurrentDirectory();
            }

            else if (!Directory.Exists(directoryPath)) // Checking if directory exists, if not, create it
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                Console.WriteLine();
                string textMessage = "Enter the text you want to write to the file: " + "(Type \"!exit\" to exit)";
                WriteMessage(textMessage);

                string text = Console.ReadLine();

                while (text != "!exit") // Write to file until user types "!exit"
                {
                    sw.WriteLine(text);
                    text = Console.ReadLine();
                }

                string fileWrittenMessage = "File saved successfully!";
                WriteMessage(fileWrittenMessage);
            }
        }

        static void Main(string[] args)
        {
            string welcomeMessage = "Welcome to Simple Text Editor!";
            WriteMessage(welcomeMessage);

            string enterPath = "Enter the file path to the text file you want to edit: " + "(Actually you are here: " + Directory.GetCurrentDirectory() + ")";
            WriteMessage(enterPath);    

            string filePath = Console.ReadLine();
            try
            {
                if (File.Exists(filePath)) // Check if file exists
                {
                    string fileExistsMessage = "File exists!";
                    WriteMessage(fileExistsMessage);

                    doYouWantToReadOrWrite:
                    string modeMessage = "Do you want to read or write to the file? (r/w)";
                    WriteMessage(modeMessage);

                    string mode = Console.ReadKey().KeyChar.ToString().ToLower();

                    if (mode == "r") // Read file if mode is "r"
                    {
                        ReadFile(filePath);
                    }

                    else if (mode == "w") // Write to file if mode is "w"
                    {
                        WriteFile(filePath);
                    }

                    else // If mode is invalid, ask again by using goto (it isn't recommended to use goto, but i used it for practice)
                    {
                        string invalidModeMessage = "\nInvalid mode!";
                        WriteMessage(invalidModeMessage);

                        goto doYouWantToReadOrWrite;
                    }
                }

                else
                {
                    string fileDoesNotExistMessage = "File does not exist!";
                    WriteMessage(fileDoesNotExistMessage);

                    string createFileMessage = "Do you want to create the file or try again or exit? (create file: \"c\", try again: \"t\", exit: \"e\")";
                    WriteMessage(createFileMessage);

                    string isCreateFile = Console.ReadKey().KeyChar.ToString().ToLower();
                    
                    if (isCreateFile == "c")
                    {
                        WriteFile(filePath);
                    }

                    else if (isCreateFile == "t")
                    {
                        Console.WriteLine();
                        Main(args);
                    }

                    else if (isCreateFile == "e")
                    {
                        string exitMessage = "\nExiting...";
                        WriteMessage(exitMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
            
        }
    }
}