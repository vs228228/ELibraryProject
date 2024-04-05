using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;

namespace ELibraryProject.Classes
{
    /// <summary>
    /// Класс, работающий с данными пользователя. Реализованы методы регистрации, входа в аккаунт
    /// и восстановление пароля
    /// </summary>
    internal static class AccountManagerClass
    {
       static SqlConnection? sqlConnection;
        public static bool EnterToSystem(string login, string password)
        {
            
            string? connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // 3 строки выше нужны т.к. у нас нет сервера и мы перекидываемся БД с одного ПК на другой
            // Потому путь получаем вот таким путём

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = $"SELECT Login, Password FROM Users WHERE Login = @login AND Password = @password";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = command.ExecuteReader();
            // 1. Создаем строку запроса; 2. Отправляем запрос; 3-4. Настраеваем параметры;
            // 5. Создаем чтением запроса

            if (reader.Read()) // Если результат запроса не пустой
            {
                sqlConnection.Close();
                reader.Close(); ;
                return true;
            }
            else
            {
                sqlConnection.Close();
                reader.Close();
                return false;
            }
        }

        public static bool RegInSystem(string name, string secondName, string email, string login, string password, string passwordAgain,
         string codeWord, string tipToCodeWord, out string message)
         {
            if(checkForNulls(name, secondName, email, login, password, passwordAgain,
                codeWord,tipToCodeWord, out message))
            {
                return false;
            }

            string? connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // строки выше создают строку подключения к БД

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = $"SELECT Email FROM Users WHERE Email = @email";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = command.ExecuteReader();


            if(reader.Read())
            {
                sqlConnection.Close();
                reader.Close();
                message = "Эта почта уже зарегистрирована";
                return false;
            }
            reader.Close();

            sqlExpression = $"SELECT Login FROM Users WHERE Login = @login";
            command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                sqlConnection.Close();
                reader.Close();
                message = "Этот логин уже занят";
                return false;
            }

            reader.Close();

            if(password != passwordAgain)
            {
                message = "Ваши пароли не совпадают";
                return false;
            }

            sqlExpression = "INSERT INTO Users (Login, Password, Email, Name, SecondName, " +
                "CodeWord, CodeWordHint) " +
                "VALUES (@Login, @Password, @Email, @Name, @SecondName, @CodeWord, @CodeWordHint)";
            command = new SqlCommand(sqlExpression, sqlConnection);
            command = new SqlCommand (sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@Login", login);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@SecondName", secondName);
            command.Parameters.AddWithValue("@CodeWord", codeWord);
            command.Parameters.AddWithValue("@CodeWordHint", tipToCodeWord);

            if(sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
                // Регистрация окончена, данные в БД
            }
            else
            {
                sqlConnection.Close();
                message = "Непредвиденная ошибка";
                return false;
            }

            
         }
 
        /// <summary>
        /// Метод вернёт true и в msg сообщение о первой пустой строке или вернёт false, если все строки не пустые
        /// </summary>
        static bool checkForNulls(string name, string secondName, string email, string login, string password, string passwordAgain,
         string codeWord, string tipToCodeWord, out string message)
        {
            if(name == "")
            {
                message = "Заполните поле имени";
                return true;
            }

            if (secondName == "")
            {
                message = "Заполните поле фамилии";
                return true;
            }

            if (email == "")
            {
                message = "Заполните поле электронной почты";
                return true;
            }

            if (login == "" )
            {
                message = "Заполните поле логина";
                return true;
            }

            if (password == "")
            {
                message = "Заполните поле пароля";
                return true;
            }

            if (passwordAgain == "")
            {
                message = "Заполните поле повторения пароля";
                return true;
            }

            if (codeWord == "")
            {
                message = "Заполните поле кодового слова";
                return true;
            }

            if (name == "")
            {
                message = "Заполните поле вопроса к кодовому слову";
                return true;
            }

            message = "Nope";
            return false;
        }

        public static bool isUserExist(string login, out string? message)
        {
            string? connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // строки выше создают строку подключения к БД

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            // Запрос на сверку строки, котору вписал пользователь, с логином/почтой
            string sqlExpression = $"SELECT CodeWordHint FROM Users WHERE Email = @email OR Login = @login";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@email", login);
            command.Parameters.AddWithValue("@login", login);
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                message = reader.GetValue(0).ToString();
                sqlConnection.Close();
                return true;
            }
            message = "Пользователь с такими данными не найден";
            return false;
        }

        public static bool isCodeWordRight(string codeWord, string login)
        {
            string? connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // строки выше создают строку подключения к БД

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            // Запрос на сверку кодового слова
            string sqlExpression = "SELECT Password FROM Users WHERE (Email = @email OR Login = @login)" +
                " and CodeWord = @codeword";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@email", login);
            command.Parameters.AddWithValue("@codeWord", codeWord);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                sqlConnection.Close();
                reader.Close();
                return true;
            }

            return false;
        }

        public static bool changePassword(string login, string password, string passwordAgain, out string msg)
        {
            if (password != passwordAgain)
            {
                msg = "Пароли не совпадают";
                return false;
            }
            string? connectionString = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");
            // строки выше создают строку подключения к БД

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            // Запрос на смену(обновление) поля Password
            string sqlExpression = "UPDATE Users SET Password = @password WHERE Email = @email or Login = @login";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@email", login);
            command.Parameters.AddWithValue("@login", login);
            if (sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
                sqlConnection.Close();
                msg = "Good";
                return true;
            }
            msg = "Неизвестная ошибка. Попробуйте ещё раз позже";
            return false;
        }

    }
}
