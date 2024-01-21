using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace ELibraryProject.PersonalAccounts
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private SqlConnection? sqlConnection;

        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Goods"].ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string is null");
            }

            connectionString = connectionString
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlExpression = "INSERT INTO Books (Title, Author, Price, Count, WritingDate) " +
                "VALUES ('1984', 'Джордж Оруэлл', 18, 20, 1949)";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
                MessageBox.Show("сюда её");
                sqlConnection.Close();
            }
        }
    }
}