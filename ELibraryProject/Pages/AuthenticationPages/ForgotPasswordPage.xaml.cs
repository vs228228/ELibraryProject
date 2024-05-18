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
    /// Логика взаимодействия для ForgotPasswordPage.xaml
    /// </summary>
    public partial class ForgotPasswordPage : Page
    {
        LoginPage loginPage;
        private string login = "";
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
            if (AccountManager.isUserExist(LoginTextBox.Text, out string? msg))
            {
                CodeWordTextBox.Visibility = Visibility.Visible;
                CodeWordLable.Visibility = Visibility.Visible;
                CodeWordTip.Content += msg;
                CodeWordTip.Visibility = Visibility.Visible;
                EnterButton.Click -= EnterButton_Click;
                EnterButton.Click += SecondHandlerFoButton;
                login = LoginTextBox.Text;

            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void SecondHandlerFoButton(object sender, RoutedEventArgs e)
        {
            // Дописать переход на след страницу в случае успеха
            if (AccountManager.isCodeWordRight(CodeWordTextBox.Text, login))
            {
                System.Windows.Forms.MessageBox.Show("Всё верно");
                 NavigationService.Navigate(new ChangePasswordPage(loginPage, login));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Кодовое слово введено неверно");
            }
        }
    }
}
