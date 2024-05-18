using System.Collections.ObjectModel;
using ELibraryProject.Classes;
using ELibraryProject.Context;

namespace ELibraryProject.Managers
{
    public class CatalogManager
    {
        public static ObservableCollection<BookView> LoadBooks()
        {
            return LoadBooksFromDatabase();
        }

        private static ObservableCollection<BookView> LoadBooksFromDatabase()
        {
            return new ObservableCollection<BookView>(BookManager.GetBookViewList());
        }

        public static BookView GetCertainBook(string TitleAndAuthor)
        {
            ObservableCollection<BookView> books = LoadBooksFromDatabase();
            string title = GetBookName(TitleAndAuthor);

            foreach (var c in books)
            {
                if (c.Title == title)
                {
                    return c;
                }
            }

            return new BookView();
        }

        public static void OutSystem()
        {
            UserContext.CurrentUser = null;
        }

        private static string GetBookName(string TitleAndAuthor)
        {
            int index = TitleAndAuthor.IndexOf(",");
            return TitleAndAuthor.Substring(0, index);
        }
    }
}
