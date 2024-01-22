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


namespace ELibraryProject
{

    delegate bool TryEnterToSystem(string login, string password);

    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        string connectiongString;

        public LoginPage()
        {
            InitializeComponent();
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }

        private TryEnterToSystem? tryToEnter = null; // нужно указать обработчик входа

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            if (tryToEnter is not null && tryToEnter(login, password) is true) // обязательно отделить проверку на null
            {
                // переход на некст страницу с учетом данных логина и пароля
            }
            else
            {
                IncorrectPasswordLable.Visibility = Visibility.Visible;
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RegAcc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(this));
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }

    }
}
