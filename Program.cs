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
                WriteMessage(wait, 25);

                while (sr.Peek() > -1)
                {
                    Console.WriteLine(sr.ReadLine());
                }

                WriteMessage(wait, 25);   
            }

            checkContinue();
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

            checkContinue();
        }

        static void checkContinue() // Ask user if they want to continue or exit
        {
            string continueMessage = "Do you want to continue? (y/n)";
            WriteMessage(continueMessage);

            string isContinue = Console.ReadKey().KeyChar.ToString().ToLower();

            if (isContinue == "y")
            {
                Console.WriteLine();
                Main(null);
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

        static void DeleteFile(string filePath) // Delete file
        {
            string deleteMessage = "\nAre you sure you want to delete the file? (y/n)";
            WriteMessage(deleteMessage);

            string isDelete = Console.ReadKey().KeyChar.ToString().ToLower();

            if (isDelete == "y")
            {
                File.Delete(filePath);
                string fileDeletedMessage = "\nFile deleted successfully!";
                WriteMessage(fileDeletedMessage);
            }

            else
            {
                string fileNotDeletedMessage = "\nFile not deleted!";
                WriteMessage(fileNotDeletedMessage);
            }

            checkContinue();
        }

        static void StartProgram()
        {
            string welcomeMessage = "Welcome to Simple Text Editor!";
            WriteMessage(welcomeMessage);

            string enterPath = "Enter the file path to the text file you want to edit: " + "(Actually you are here: " + Directory.GetCurrentDirectory() + ")";
            WriteMessage(enterPath);

            string filePath = Console.ReadLine();
            try
            {
                if (File.Exists(filePath))
                {
                    string fileExistsMessage = "File exists!";
                    WriteMessage(fileExistsMessage);

                    doYouWantToReadOrWriteOrDelete:
                    string modeMessage = "Do you want to read or write or delete the file? (read: \"r\", write: \"w\", delete: \"d\")";
                    WriteMessage(modeMessage);

                    string mode = Console.ReadKey().KeyChar.ToString().ToLower();

                    if (mode == "r")
                    {
                        ReadFile(filePath);
                    }

                    else if (mode == "w")
                    {
                        WriteFile(filePath);
                    }

                    else if (mode == "d")
                    {
                        DeleteFile(filePath);
                    }

                    else
                    {
                        string invalidModeMessage = "\nInvalid mode!";
                        WriteMessage(invalidModeMessage);

                        goto doYouWantToReadOrWriteOrDelete;
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
                        Main(null);
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


        static void Main(string[] args)
        {
            Console.Clear();
            StartProgram();
        }
    }
}