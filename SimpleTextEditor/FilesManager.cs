using System;
using System.IO;

namespace SimpleTextEditor
{
    class FilesManager
    {
        // Read file
        public static void ReadFile(string filePath) 
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Console.WriteLine();
                string wait = "--------------------";
                UI.WriteMessage(wait, 25);

                while (sr.Peek() > -1)
                {
                    Console.WriteLine(sr.ReadLine());
                }

                UI.WriteMessage(wait, 25);   
            }

            UI.checkContinue();
        }

        // Write to file
        public static void WriteFile(string filePath) 
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
                UI.WriteMessage(textMessage);

                string text = Console.ReadLine();

                while (text != "!exit") // Write to file until user types "!exit"
                {
                    sw.WriteLine(text);
                    text = Console.ReadLine();
                }

                string fileWrittenMessage = "File saved successfully!";
                UI.WriteMessage(fileWrittenMessage);
            }

            UI.checkContinue();
        }

        // Delete file
        public static void DeleteFile(string filePath)
        {
            string deleteMessage = "\nAre you sure you want to delete the file? (y/n)";
            UI.WriteMessage(deleteMessage);

            string isDelete = Console.ReadKey().KeyChar.ToString().ToLower();

            if (isDelete == "y")
            {
                File.Delete(filePath);
                string fileDeletedMessage = "\nFile deleted successfully!";
                UI.WriteMessage(fileDeletedMessage);
            }

            else
            {
                string fileNotDeletedMessage = "\nFile not deleted!";
                UI.WriteMessage(fileNotDeletedMessage);
            }

            UI.checkContinue();
        }
    }
}