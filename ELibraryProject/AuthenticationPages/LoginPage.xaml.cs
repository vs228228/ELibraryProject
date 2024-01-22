using System;
using Microsoft;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Data.SqlClient;
using ELibraryProject.Classes;


namespace ELibraryProject
{

    delegate bool TryEnterToSystem(string login, string password);

    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        private SqlConnection? sqlConnection;
        private TryEnterToSystem? tryToEnter = AccountManagerClass.EnterToSystem; // нужно указать обработчик входа

        public LoginPage()
        {
            InitializeComponent();
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }

        

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            if (tryToEnter is not null && tryToEnter(login, password) is true) // обязательно отделить проверку на null
            {
                // переход на некст страницу с учетом данных логина и пароля
                MessageBox.Show("Дальше должна быть загружена страница ЛК");
            }
            else
            {
                IncorrectPasswordLable.Visibility = Visibility.Visible;
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
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

            string sqlExpression = "INSERT INTO UsersInfo (Login, Password) " +
                "VALUES ('Lalka', '12344321')";

            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
                MessageBox.Show("сюда её");
                sqlConnection.Close();
            }
        }


        private void RegAcc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(this));
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }

    }
}
