using ELibraryProject.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace ELibraryProject.Databases
{
    internal class DatabaseHandler
    {
        private static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }
            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            return connectionString;
        }

        private static SqlConnection GetSqlConnection()
        {
            SqlConnection? sqlConnection;

            string connectionString = GetConnectionString();

            sqlConnection = new SqlConnection(connectionString);

            if (sqlConnection == null)
            {
                throw new InvalidOperationException("SQL connection object is null");
            }

            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("SQL connection not open");
            }
            return sqlConnection;
        }

        //public static List<Book> GetBooks()
        //{
        //    SqlConnection? sqlConnection;
        //    string connectionString = ConfigurationManager.ConnectionStrings["Goods"].ConnectionString;
        //    if (connectionString == null)
        //    {
        //        throw new InvalidOperationException("Connection string is null");
        //    }

        //    connectionString = connectionString
        //        .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
        //        .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");

        //    sqlConnection = new SqlConnection(connectionString);

        //    return " ";
        //}

        public static void AddBook(Book book)
        {
            SqlConnection sqlConnection = GetSqlConnection();

            string sqlExpression =
                "INSERT INTO Books (Title, Author, PageCount, " +
                "Price, Count, PublicationDate, CoverType, Publisher, " +
                "Description, PicturePath, Category) " +
                $"VALUES ('{book.Title}', " +
                $"'{book.Author}', " +
                $"{book.PageCount}, " +
                $"{book.Price}, " +
                $"{book.Count}, " +
                $"{book.PublicationDate}, " +
                $"'{book.CoverType}', " +
                $"'{book.Publisher}', " +
                $"'{book.Description}', " +
                $"'{book.PicturePath}', " +
                $"'{book.Category}')";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
