using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your book library!\nTo see the command options type \"Help\".");
            string input = Console.ReadLine();

            if (input.ToLower().Equals("Help"))
            {
                Console.WriteLine("Commands available:" +
                    "\nHelp - displays all the commands" +
                    "\nAddBook - adds a book to the library\n" +
                    "\nSeeLibary - displays all the books found in the library");
            }
        }
    }
}
