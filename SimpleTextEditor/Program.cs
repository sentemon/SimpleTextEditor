using System;
using System.IO;

namespace SimpleTextEditor
{
    public class Program
    {
        // Start program (I could have used Main method but I wanted to use this method for my own learning purposes)
        public static void StartProgram()
        {
            string welcomeMessage = "Welcome to Simple Text Editor!";
            UI.WriteMessage(welcomeMessage);

            string enterPath = "Enter the file path to the text file you want to edit: " + "(Actually you are here: " + Directory.GetCurrentDirectory() + ")";
            UI.WriteMessage(enterPath);

            string filePath = Console.ReadLine();
            try
            {
                if (File.Exists(filePath))
                {
                    string fileExistsMessage = "File exists!";
                    UI.WriteMessage(fileExistsMessage);

                    doYouWantToReadOrWriteOrDelete: // I know it's not a good practice but I want to use it for my own learning purposes
                    string modeMessage = "Do you want to read or write or delete the file? (read: \"r\", write: \"w\", delete: \"d\")";
                    UI.WriteMessage(modeMessage);

                    string mode = Console.ReadKey().KeyChar.ToString().ToLower();

                    if (mode == "r")
                    {
                        FilesManager.ReadFile(filePath);
                    }

                    else if (mode == "w")
                    {
                        FilesManager.WriteFile(filePath);
                    }

                    else if (mode == "d")
                    {
                        FilesManager.DeleteFile(filePath);
                    }

                    else
                    {
                        string invalidModeMessage = "\nInvalid mode!";
                        UI.WriteMessage(invalidModeMessage);

                        goto doYouWantToReadOrWriteOrDelete; // I know it's not a good practice but I want to use it for my own learning purposes
                    }
                }

                else
                {
                    string fileDoesNotExistMessage = "File does not exist!";
                    UI.WriteMessage(fileDoesNotExistMessage);

                    string createFileMessage = "Do you want to create the file or try again or exit? (create file: \"c\", try again: \"t\", exit: \"e\")";
                    UI.WriteMessage(createFileMessage);

                    string isCreateFile = Console.ReadKey().KeyChar.ToString().ToLower();
                    
                    if (isCreateFile == "c")
                    {
                        FilesManager.WriteFile(filePath);
                    }

                    else if (isCreateFile == "t")
                    {
                        Console.WriteLine();
                        Main(null);
                    }

                    else if (isCreateFile == "e")
                    {
                        string exitMessage = "\nExiting...";
                        UI.WriteMessage(exitMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        public static void Main(string[] args)
        {
            Console.Clear(); // Clear console for better user experience
            StartProgram(); // Here we go!
        }
    }
}