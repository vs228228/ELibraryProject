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
using System.IO;

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

        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            

            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Books";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new()
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
                        Category = reader["Category"].ToString()
                    };
                    books.Add(book);
                }
            }

            return books;
        }

        public static void AddBook(Book book)

        {
            string sqlExpression = "INSERT INTO Books (Title, Author, PageCount, Price, Count, PublicationDate, CoverType, Publisher, Description, PicturePath, Category) " +
                       "VALUES (@Title, @Author, @PageCount, @Price, @Count, @PublicationDate, @CoverType, @Publisher, @Description, @PicturePath, @Category)";

            using (var connection = GetSqlConnection())
            {
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PageCount", book.PageCount);
                    command.Parameters.AddWithValue("@Price", book.Price);
                    command.Parameters.AddWithValue("@Count", book.Count);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);
                    command.Parameters.AddWithValue("@CoverType", book.CoverType);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@Description", book.Description);
                    command.Parameters.AddWithValue("@PicturePath", book.PicturePath);
                    command.Parameters.AddWithValue("@Category", book.Category);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateBook(Book book)
        {
            string sqlExpression = "UPDATE Books SET Title = @Title, Author = @Author, PageCount = @PageCount, Price = @Price, Count = @Count, " +
                                   "PublicationDate = @PublicationDate, CoverType = @CoverType, Publisher = @Publisher, Description = @Description, " +
                                   "PicturePath = @PicturePath, Category = @Category WHERE Id = @Id";

            using (var connection = GetSqlConnection())
            {
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PageCount", book.PageCount);
                    command.Parameters.AddWithValue("@Price", book.Price);
                    command.Parameters.AddWithValue("@Count", book.Count);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);
                    command.Parameters.AddWithValue("@CoverType", book.CoverType);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@Description", book.Description);
                    command.Parameters.AddWithValue("@PicturePath", book.PicturePath);
                    command.Parameters.AddWithValue("@Category", book.Category);
                    command.Parameters.AddWithValue("@Id", book.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Пока не тестил, может не работать
        public static void DeleteBook(Book book)
        {
            SqlConnection sqlConnection = GetSqlConnection();
            string sqlExpression = "DELETE FROM Books WHERE Id = @Id";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@Id", book.Id);
            command.ExecuteNonQuery();
            sqlConnection.Close();

            File.Delete(book.PicturePath);
        }

        public static void GetUsers()
        {
            List<User> users = new List<User>();


            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Users";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email = reader["Email"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        CodeWord = reader["CodeWord"].ToString(),
                        TipToCodeWord = reader["CodeWordHint"].ToString(),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
                    };
                    users.Add(user);
                }
            }
        }

        public static void AddUser(User user)

        {
            string sqlExpression = "INSERT INTO Books (Login, Password, Email, FirstName, LastName, CodeWord, TipToCodeWord, IsAdmin) " +
                       "VALUES (@Login, @Password, @Email, @FirstName, @LastName, @CodeWord, @TipToCodeWord, @IsAdmin)";

            using (var connection = GetSqlConnection())
            {
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@CodeWord", user.CodeWord);
                    command.Parameters.AddWithValue("@TipToCodeWord", user.TipToCodeWord);
                    command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
