using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace ELibraryProject.Classes
{
    class CatalogManager
    {
        public ObservableCollection<Book>  LoadBooks()
        {
            string connectionString = getConnectionString();
            ObservableCollection<Book> books = LoadBooksFromDatabase(connectionString);

            return books;
        }

        private ObservableCollection<Book> LoadBooksFromDatabase(string connectionString)
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
           SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = "SELECT * FROM Books";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Book book = new Book(reader["Title"].ToString(), reader["Author"].ToString(), (decimal)reader["Price"]);

                books.Add(book);
                
            }
            sqlConnection.Close();
            reader.Close();
            return books;
        }

        public Book GetCertainBook(string TitleAndAuthor)
        {
            string connectionString = getConnectionString();
            string title = GetBookName(TitleAndAuthor);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = "SELECT * FROM Books WHERE Title = @title";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@title", title);
            
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            Book book = new Book(reader["Title"].ToString(), reader["Author"].ToString(), (decimal)reader["Price"]);

            return book;

        }

        private string getConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // 3 строки выше нужны т.к. у нас нет сервера и мы перекидываемся БД с одного ПК на другой
            // Потому путь получаем вот таким путём

            return connectionString;
        }

        private string GetBookName(string TitleAndAuthor)
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
