using ELibraryProject.Managers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELibraryProject.AuthenticationPages
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {

        private LoginPage loginPage;
        private string login;
        public ChangePasswordPage(LoginPage loginPage, string login)
        {
            this.loginPage = loginPage;
            this.login = login;
            InitializeComponent();
        }

        private void BackLable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(loginPage);
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            string passwordAgain = PasswordAgainTextBox.Text;
            if(AccountManager.changePassword(login, password, passwordAgain, out string msg))
            {
                MessageBox.Show("Пароль успешно изменён");
                NavigationService.Navigate(loginPage);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
    }
}
