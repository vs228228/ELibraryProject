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
using ELibraryProject.Database.Models;

namespace ELibraryProject.Database
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
                connection.Close();
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
                    connection.Close();
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
                    connection.Close();
                }
            }
        }

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

        public static List<User> GetUsers()
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
                connection.Close();
            }
            return users;
        }

        public static User GetUserByLogin(string login)
        {
            User user;

            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Users WHERE Login = @login";
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@login", login);

                    using var reader = command.ExecuteReader();
                    reader.Read();
                    user = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Login = Convert.ToString(reader["Login"]),
                        Password = Convert.ToString(reader["Password"]),
                        Email = Convert.ToString(reader["Email"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        CodeWord = Convert.ToString(reader["CodeWord"]),
                        TipToCodeWord = Convert.ToString(reader["CodeWordHint"]),
                        IsAdmin = Convert.ToBoolean(reader["isAdmin"])
                    };

                    connection.Close();
                }
            }

            return user;
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
                    connection.Close();
                }
            }
        }

        public static void AddOrder(Order order)
        {
            string sqlExpression = "INSERT INTO Orders (UserId, BookId, Number, OrderDate, ApprovalDate, CancellationDate, CompletionDate) " +
                       "VALUES (@UserId, @BookId, @Number, @OrderDate, @ApprovalDate, @CancellationDate, @CompletionDate)";

            using (var connection = GetSqlConnection())
            {
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@UserId", order.UserId);
                    command.Parameters.AddWithValue("@BookId", order.BookId);
                    command.Parameters.AddWithValue("@Number", order.Number);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@ApprovalDate", (object?)order.ApprovalDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CancellationDate", (object?)order.CancellationDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CompletionDate", (object?)order.CompletionDate ?? DBNull.Value);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static void UpdateOrder(Order order)
        {
            string sqlExpression = "UPDATE Orders SET UserId = @UserId, BookId = @BookId, Number = @Number, " +
                "OrderDate = @OrderDate, ApprovalDate = @ApprovalDate, CancellationDate = @CancellationDate, " +
                "CompletionDate = @CompletionDate WHERE Id = @Id";
            using (var connection = GetSqlConnection())
            {
                using (var command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@UserId", order.UserId);
                    command.Parameters.AddWithValue("@BookId", order.BookId);
                    command.Parameters.AddWithValue("@Number", order.Number);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@ApprovalDate", (object?)order.ApprovalDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CancellationDate", (object?)order.CancellationDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CompletionDate", (object?)order.CompletionDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Id", order.Id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static List<Order> GetOrders()
        {
            List<Order> orders = new();
            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Orders";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Number = Convert.ToInt32(reader["Number"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovalDate"]) : null,
                        CancellationDate = reader["CancellationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CancellationDate"]) : null,
                        CompletionDate = reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletionDate"]) : null
                    };
                    orders.Add(order);
                }
                connection.Close();
            }
            return orders;
        }
        public static List<Order> GetUnapprovedOrders()
        {
            List<Order> orders = new();
            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Orders WHERE ApprovalDate IS NULL AND CancellationDate IS NULL";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Number = Convert.ToInt32(reader["Number"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovalDate"]) : null,
                        CancellationDate = reader["CancellationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CancellationDate"]) : null,
                        CompletionDate = reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletionDate"]) : null,
                    };
                    orders.Add(order);
                }
                connection.Close();
            }
            return orders;
        }

        public static List<Order> GetApprovedOrders()
        {
            List<Order> orders = new();
            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Orders WHERE ApprovalDate IS NOT NULL AND CancellationDate IS NULL AND CompletionDate IS NULL";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Number = Convert.ToInt32(reader["Number"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovalDate"]) : null,
                        CancellationDate = reader["CancellationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CancellationDate"]) : null,
                        CompletionDate = reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletionDate"]) : null
                    };
                    orders.Add(order);
                }
                connection.Close();
            }
            return orders;
        }

        public static List<Order> GetArchivedOrders()
        {
            List<Order> orders = new();
            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Orders WHERE CompletionDate IS NOT NULL OR CancellationDate IS NOT NULL";
                using var command = new SqlCommand(sqlExpression, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Number = Convert.ToInt32(reader["Number"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovalDate"]) : null,
                        CancellationDate = reader["CancellationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CancellationDate"]) : null,
                        CompletionDate = reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletionDate"]) : null
                    };
                    orders.Add(order);
                }
                connection.Close();
            }
            return orders;
        }

        public static Order GetOrderById(int id)
        {
            using (var connection = GetSqlConnection())
            {
                string sqlExpression = "SELECT * FROM Orders WHERE Id = @Id";
                using var command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@Id", id);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Order
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Number = Convert.ToInt32(reader["Number"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovalDate"]) : null,
                        CancellationDate = reader["CancellationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CancellationDate"]) : null,
                        CompletionDate = reader["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletionDate"]) : null
                    };
                }
                else
                {
                    return null; // Возвращаем null, если запись с указанным Id не найдена
                }
            }
        }
    }
}
