using ELibraryProject.Classes;
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
    /// Логика взаимодействия для ForgotPasswordPage.xaml
    /// </summary>
    public partial class ForgotPasswordPage : Page
    {
        LoginPage loginPage;
        public ForgotPasswordPage(LoginPage loginPage)
        {
            InitializeComponent();
            this.loginPage = loginPage;
        }

        private void BackLable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(loginPage);
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountManagerClass.isUserExist(LoginTextBox.Text, out string? msg))
            {
                CodeWordTextBox.Visibility = Visibility.Visible;
                CodeWordLable.Visibility = Visibility.Visible;
                CodeWordTip.Content += msg;
                CodeWordTip.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
    }
}
