using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    class BookView : Book
    {
        public BookView() { }

        public string TitleAndAuthor { get; set; }

        public Image Picture { get; set; }
    }
}
