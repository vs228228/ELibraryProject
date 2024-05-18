using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using ELibraryProject.Classes;
using ELibraryProject.Context;
using ELibraryProject.Database;
using ELibraryProject.Database.Models;
using static System.Reflection.Metadata.BlobBuilder;

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
            List<Book> oldBooks = DatabaseHandler.GetBooks();
            ObservableCollection<BookView> books = new ObservableCollection<BookView>();
            foreach (Book book in oldBooks)
            {
                BookView babook = new BookView()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PageCount = book.PageCount,
                    Price = book.Price,
                    Count = book.Count,
                    PublicationDate = book.PublicationDate,
                    CoverType = book.CoverType,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    PicturePath = book.PicturePath.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("\\bin\\Debug\\net8.0-windows\\", ""),
                    Category = book.Category
                };
                babook.TitleAndAuthor = book.Title + ", " + book.Author;
                babook.Picture = Image.FromFile(book.PicturePath.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("\\bin\\Debug\\net8.0-windows\\", ""));
                books.Add(babook);
            }

            return books;
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
