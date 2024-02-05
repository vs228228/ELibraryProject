using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    class Book
    {
        public string NameAndAuthor { get; set; }

        public Book(string BookName, string Author, string Description, decimal price)
        {
            this.BookName = BookName;
            this.Author = Author;
            this.Description = Description;
            this.Price = price;
            NameAndAuthor = BookName + ", " + Author;
        }

        public Book(string BookName, string Author, decimal price)
        {
            this.BookName = BookName;
            this.Author = Author;
            this.Price = price;
            NameAndAuthor += BookName + ", " + Author;
            this.Description = "";
        }

        public string BookName { get; set; }

        public string Author { get; set; }

        public string Description {  get; set; }

        public decimal Price {  get; set; }

    }
}
