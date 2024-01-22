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
        static public bool EnterToSystem(string login, string password)
        {
            SqlConnection? sqlConnection;
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

       /* static bool RegInSystem()
        {
            
        }*/

    }
}
