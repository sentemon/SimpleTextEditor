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

            if (!Directory.Exists(directoryPath)) // Checking if directory exists, if not, create it
                Directory.CreateDirectory(directoryPath);

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                Console.WriteLine();
                string textMessage = "Enter the text you want to write to the file: ";
                WriteMessage(textMessage);

                string text = Console.ReadLine();
                sw.WriteLine(text);
            }
        }

        static void Main(string[] args)
        {
            string welcomeMessage = "Welcome to Simple Text Editor!";
            WriteMessage(welcomeMessage);

            string enterPath = "Enter the file path to the text file you want to edit: ";
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

                    string createFileMessage = "Do you want to create the file? (y/n)";
                    WriteMessage(createFileMessage);

                    string isCreateFile = Console.ReadKey().KeyChar.ToString().ToLower();
                    bool createFile = isCreateFile == "y" ? true : false;

                    if (createFile)
                    {
                        WriteFile(filePath);
                    }

                    else
                    {
                        string exitMessage = "Exiting...";
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