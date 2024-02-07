using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    class Book
    {

        public Book(string Title, string Author, string Description, decimal price)
        {
            this.Title = Title;
            this.Author = Author;
            this.Description = Description;
            this.Price = price;
            TitleAndAuthor = Title + ", " + Author;
        }

        public Book(string Title, string Author, decimal price)
        {
            this.Title = Title;
            this.Author = Author;
            this.Price = price;
            TitleAndAuthor = Title + ", " + Author;
            this.Description = "";
        }

        public string TitleAndAuthor { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description {  get; set; }

        public decimal Price {  get; set; }

    }
}
