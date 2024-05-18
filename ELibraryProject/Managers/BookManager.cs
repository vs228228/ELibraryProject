using ELibraryProject.Classes;
using ELibraryProject.Database.Models;
using ELibraryProject.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ELibraryProject.Managers
{
    internal class BookManager
    {
        public static List<BookView> GetBookViewList()
        {
            List<Book> books = DatabaseHandler.GetBooks();
            List<BookView> bookViews = new List<BookView>();
            foreach (Book book in books)
            {
                BookView bookView = new BookView()
                {
                    Id = book.Id,
                    PageCount = book.PageCount,
                    Author = book.Author,
                    Title = book.Title,
                    TitleAndAuthor = book.Title + ", " + book.Author,
                    Price = book.Price,
                    Count = book.Count,
                    PublicationDate = book.PublicationDate,
                    CoverType = book.CoverType,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    Category = book.Category,
                    PicturePath = book.PicturePath.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("\\bin\\Debug\\net8.0-windows\\", "")
                };

                using (var fileStream = new FileStream(book.PicturePath.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("\\bin\\Debug\\net8.0-windows\\", ""), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = fileStream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    bookView.Picture = bitmap;
                }
                bookViews.Add(bookView);
            }
            return bookViews;
        }
    }
}
