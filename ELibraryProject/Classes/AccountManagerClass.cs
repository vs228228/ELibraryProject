using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace ELibraryProject.Classes
{
    internal class AccountManagerClass
    {
       static SqlConnection? sqlConnection;
        static public bool EnterToSystem(string login, string password)
        {
            
            string? connectionString = ConfigurationManager.ConnectionStrings["UserInfo"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = $"SELECT Login, Password FROM UsersInfo WHERE Login = @login AND Password = @password";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                sqlConnection.Close();
                return true;
            }
            else
            {
                sqlConnection.Close();
                return false;
            }
        }

        static public bool RegInSystem(string name, string secondName, string email, string login, string password, string passwordAgain,
         string codeWord, string tipToCodeWord, out string message)
         {
            if(checkForNulls(name, secondName, email, login, password, passwordAgain,
                codeWord,tipToCodeWord, out message))
            {
                return false;
            }

            string? connectionString = ConfigurationManager.ConnectionStrings["UserInfo"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = $"SELECT Email FROM UsersInfo WHERE Email = @email";

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

            sqlExpression = $"SELECT Login FROM UsersInfo WHERE Login = @login";
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

            sqlExpression = "INSERT INTO UsersInfo (Login, Password, Email, Name, SecondName, " +
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
            }
            else
            {
                sqlConnection.Close();
                message = "Непредвиденная ошибка";
                return false;
            }

            
         }

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

    }
}
