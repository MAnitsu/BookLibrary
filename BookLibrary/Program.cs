using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static BookLibrary.Program;

namespace BookLibrary
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            // set the path for the txt file where the books will be stored
            string path = "BooksDisplay.txt"; // enter your own path
            
            // checks if the txt file exists
            if (File.Exists(path))
            {
                // the bookCount will be equal to the number of lines (books) from the library
                int bookCount = File.ReadAllLines(path).Length;

                // a loop that will ask the user for commands
                while (true)
                {
                    Console.WriteLine("Welcome to your book library!\n\n" + Help());
                    string input = Console.ReadLine().ToLower();

                    // action for the help command
                    if (input.Equals("help"))
                    {
                        // restarts the loop with the help message
                        continue;
                    }
                    // action for the exit command
                    else if (input.Equals("exit"))
                    {
                        Environment.Exit(0);
                    }
                    // action for the addbook command
                    else if (input.Equals("addbook"))
                    {
                        // display a message asking the user for details about the book
                        Console.WriteLine("Now you can add a book to the library by typing the title, author, genre, publisher and page number\n" +
                                             "Each parameter should be followed by a comma (,)\n" +
                                             "Only the title is mandatory, the rest of the parameters are optional\n");
                        string addBookInput = Console.ReadLine();

                        // add the book to the library - if there is any book added already use a new line
                        if (bookCount == 0)
                        {
                            File.AppendAllText(path, addBookInput);
                        }
                        else
                        {
                            File.AppendAllText(path, Environment.NewLine + addBookInput);
                        }
                        // increment the counter for the books so the app will know when to add new lines in the txt file
                        bookCount += 1;

                        // insert the retry message so the user can start over
                        string retry = RetryMessage();

                        if (retry.Equals("y"))
                        {
                            Console.WriteLine();
                            continue;
                        }
                        break;
                    }
                    else if (input.Equals("seelibrary"))
                    {
                        // display the book library
                        Console.WriteLine("\n" + File.ReadAllText(path) + "\n");

                        // insert the retry message so the user can start over
                        string retry = RetryMessage();

                        if (retry.Equals("y"))
                        {
                            Console.WriteLine();
                            continue;
                        }
                        break;
                    }
                    else
                    { 
                        // if the command entered is none of the correct ones, start over
                        Console.WriteLine("\nThe command entered does not exist. Try again!\n");
                        continue;
                    }
                }
            }
            // if the txt file that stores the books is missing show a friendly message to the user
            else
            {
                Console.WriteLine("The \"BooksDisplay.txt\" file is missing, please create one");
            }
        }

        static string Help()
        {
            return ("Commands available:\n" +
                    "Help - displays all the commands\n" +
                    "AddBook - adds a book to the library, \n" +
                    "SeeLibary - displays all the books found in the library\n" +
                    "Exit - closes the console\n" +
                    "Try entering one of the commands bellow: ");
        }

        static string RetryMessage()
        {
            Console.WriteLine("Would you like to try another command? y/n");
            string inputRetry = Console.ReadLine().ToLower();
            return inputRetry;
        }
    }
}
