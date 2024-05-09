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
using ELibraryProject.Database;
using static System.Reflection.Metadata.BlobBuilder;

namespace ELibraryProject.Classes
{
    public class CatalogManager
    {
        public static ObservableCollection<BookView> LoadBooks()
        {
            return LoadBooksFromDatabase();
        }

        private static ObservableCollection<BookView> LoadBooksFromDatabase()
        {
         /*   ObservableCollection<Book> books = new ObservableCollection<Book>();
           SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = "SELECT * FROM Books";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                //  Book book = new Book(reader["Title"].ToString(), reader["Author"].ToString(), reader["Description"].ToString(), (decimal)reader["Price"]);

                Book book = new Book()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString(),
                    Author = reader["Author"].ToString(),
                    PageCount = Convert.ToInt16(reader["PageCount"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Count = Convert.ToInt32(reader["Count"]),
                    PublicationDate = Convert.ToInt16(reader["PublicationDate"]),
                    CoverType = reader["CoverType"].ToString(),
                    Publisher = reader["Publisher"].ToString(),
                    Description = reader["Description"].ToString(),
                    PicturePath = reader["PicturePath"].ToString(),
                    Category = reader["Category"].ToString(),
                    TitleAndAuthor = reader["Title"].ToString() + ", " + reader["Author"].ToString(),
                  //  Picture = Image.FromFile((reader["PicturePath"].ToString())) 

                };
                books.Add(book);

                MessageBox.Show("D:\\ELibrary\\ELibraryProject\\ELibraryProject\\Databases\\Pictures\\12-2.jpeg");
                MessageBox.Show((reader["PicturePath"].ToString()));
         
            }
            sqlConnection.Close();
            reader.Close();
            return books;
         */

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
           /* string connectionString = getConnectionString();
            string title = GetBookName(TitleAndAuthor);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = "SELECT * FROM Books WHERE Title = @title";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@title", title);
            
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            //  Book book = new Book(reader["Title"].ToString(), reader["Author"].ToString(), reader["Description"].ToString(), (decimal)reader["Price"]);
            Book book = new Book()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                PageCount = Convert.ToInt16(reader["PageCount"]),
                Price = Convert.ToDecimal(reader["Price"]),
                Count = Convert.ToInt32(reader["Count"]),
                PublicationDate = Convert.ToInt16(reader["PublicationDate"]),
                CoverType = reader["CoverType"].ToString(),
                Publisher = reader["Publisher"].ToString(),
                Description = reader["Description"].ToString(),
                PicturePath = reader["PicturePath"].ToString(),
                Category = reader["Category"].ToString(),
                TitleAndAuthor = reader["Title"].ToString() + ", " + reader["Author"].ToString(),
             //   Picture = Image.FromFile(reader["PicturePath"].ToString())
            };
            
            MessageBox.Show("D:\\ELibrary\\ELibraryProject\\ELibraryProject\\Databases\\Pictures\\12-2.jpeg");
            MessageBox.Show((reader["PicturePath"].ToString()));
           */

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

        private static string GetBookName(string TitleAndAuthor)
        {
            int index = TitleAndAuthor.IndexOf(",");
            /* KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>("login", "password");
            string json = JsonSerializer.Serialize(keyValuePair);
            System.Windows.Forms.MessageBox.Show(json);
            KeyValuePair<string,string> newPair =  JsonSerializer.Deserialize<KeyValuePair<string,string>>(json);
            System.Windows.Forms.MessageBox.Show(newPair.Value); */
            return TitleAndAuthor.Substring(0, index);
        }
    }
}
