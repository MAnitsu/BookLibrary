namespace BookLibrary
{
    internal partial class Program
    {
        public class Book
        {
            private string Title;
            private string Author;
            private string Genre;
            private string Publisher;
            private int PageNr;

            // created multiple constructors so only the title is mandatory and the other details are optional
            public Book(string title)
            {
                Title = title;
            }
            public Book(string title, string author)
            {
                Title = title;
                Author = author;
            }
            public Book(string title, string author, string genre)
            {
                Title = title;
                Author = author;
                Genre = genre;
            }
            public Book(string title, string author, string genre, string publisher)
            {
                Title = title;
                Author = author;
                Genre = genre;
                Publisher = publisher;
            }
            public Book(string title, string author, string genre, string publisher, int pageNr)
            {
                Title = title;
                Author = author;
                Genre = genre;
                Publisher = publisher;
                PageNr = pageNr;
            }
        }
    }
}
