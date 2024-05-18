using System.Windows.Media.Imaging;
using ELibraryProject.Database.Models;

namespace ELibraryProject.Classes
{
    public class BookView : Book
    {
        public string TitleAndAuthor { get; set; }
        public BitmapImage Picture { get; set; }
    }
}
