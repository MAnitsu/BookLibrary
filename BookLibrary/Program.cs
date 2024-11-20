using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
                            File.AppendAllText(path, (bookCount + 1) + "." + addBookInput);
                        }
                        else
                        {
                            File.AppendAllText(path, Environment.NewLine + (bookCount + 1) + "." + addBookInput);
                        }
                        // increment the counter for the books so the app will know when to add new lines in the txt file
                        bookCount += 1;

                        // insert the retry message so the user can start over
                        string action = RetryMessage();

                        if (action.Equals("y"))
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

                        // insert the message asking the user if they want to edit/delete an entry
                        string action = EditDelete();

                        // convert the contents of the BookLibrary.txt to a library
                        var myDictionary = DictionaryConvertor(File.ReadAllText(path));

                        // exit the loop if they don't want to edit/delete
                        if (action.Equals("n"))
                        {
                            break;
                        }
                        // perform edit action if they want to edit
                        else if (action.Equals("edit"))
                        {
                            // ask the user which line they want modified
                            Console.Write("\nWhich line would you like to edit? ");
                            string lineToEdit = Console.ReadLine();

                            // ask the user what to be inserted in the edited line
                            Console.Write("Insert a title, author, genre, publisher or page number. ");
                            string textToEdit = Console.ReadLine();

                            // edit per key-value pair of the dictionary
                            myDictionary[lineToEdit] = textToEdit;
                         
                            // Erase the file content by overwriting it with an empty string
                            File.WriteAllText(path, string.Empty);
                            // insert the new edited string in the file
                            File.AppendAllText(path, StringConvertor(myDictionary));
                        }
                        // perform delete action if they want to delete
                        else if (action.Equals("delete"))
                        {
                            // ask the user which line they want modified
                            Console.Write("Which line would you like to delete? ");
                            string lineToDelete = Console.ReadLine();

                            // delete per key-value pair of the dictionary
                            myDictionary.Remove(lineToDelete);

                            // Erase the file content by overwriting it with an empty string
                            File.WriteAllText(path, string.Empty);
                            // insert the new edited string in the file
                            File.AppendAllText(path, StringConvertor(myDictionary));
                        }
                        else
                        {
                            // if the command entered is none of the correct ones, start over
                            Console.WriteLine("\nThe command entered does not exist. Try again!\n");
                            continue;
                        }
                        // TODO: convert the dictionary back to string after deleting/editing
                        // and insert it back into the BookDisplay.txt after erasing the old data
                        // use this function after editing or deleting

                        // insert the retry message so the user can start over
                        string actionRetry = RetryMessage();

                        if (actionRetry.Equals("y"))
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
        // a function used for the retry message
        static string RetryMessage()
        {
            Console.WriteLine("\nWould you like to try another command? y/n");
            string inputRetry = Console.ReadLine().ToLower();
            return inputRetry;
        }
        
        static string EditDelete()
        {
            Console.WriteLine("Would you like to edit or delete any entry from the library? edit/delete/n");
            string inputRetry = Console.ReadLine().ToLower();
            return inputRetry;
        }
        // a function that converts the text from the library to a dictionary so I can delte/edit it
        static Dictionary<string, string> DictionaryConvertor(string input)
        {
            // initialize the dictionary
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            // split the text into key-value pairs based on new lines
            string[] pairs = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in pairs)
            {
                // split each pair into key and value based on a dot
                string[] keyValue = pair.Split('.');
                if (keyValue.Length == 2) // ensure there are exactly 2 parts
                {
                    string key = keyValue[0];
                    string value = keyValue[1];

                    // add the key-value pair to the dictionary
                    dictionary[key] = value;
                }
                else
                {
                    Console.WriteLine($"Invalid pair: {pair}");
                }
            }

            return dictionary;
        }
        // a function that take this dictionary as a parameter and converts it back to a string format,
        // with each key-value pair being on a separate row so I can add it back to the library
        static string StringConvertor(Dictionary<string, string> dictionary)
        {
            return string.Join(Environment.NewLine, dictionary.Select(kv => kv.Key + "." + kv.Value));
        }
    }
}
