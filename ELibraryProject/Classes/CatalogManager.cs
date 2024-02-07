using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Data.SqlClient;

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

        private string getConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Goods"].ConnectionString;
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
    }
}
