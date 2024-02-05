using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    class DoubleBook
    {
        public DoubleBook() { }
        public DoubleBook(Book First, Book Second)
        {
            FirstBook = First;
            SecondBook = Second;
        }
        public Book FirstBook { get; set; }
        public Book SecondBook { get; set; }

    }
}
