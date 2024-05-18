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
using ELibraryProject.AuthenticationPages;
using System.IO;
using System.Text.Json;


namespace ELibraryProject
{

    delegate bool TryEnterToSystem(string login, string password, out bool isAdmin);

    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

     //   private SqlConnection? sqlConnection;
        private TryEnterToSystem? tryToEnter = AccountManagerClass.TryEnterToSystem; // нужно указать обработчик входа
        MainWindow mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
            this.mainWindow = mainWindow;
            AccountManagerClass.tryToUseRecordedPassword(mainWindow);
        }



        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            if (tryToEnter is not null && tryToEnter(login, password, out bool isAdmin) is true) // обязательно отделить проверку на null
            {
                // переход на некст страницу с учетом данных логина и пароля
                //  MessageBox.Show("Дальше должна быть загружена страница ЛК");
                if(RememberCheckBox.IsChecked == true && isAdmin is false)
                {
                    AccountManagerClass.writeInfoToFile(login, password);
                }
                else
                {
                    AccountManagerClass.writeInfoToFile("", "");
                }

                if (isAdmin is true)
                {
                    new EmployeeWindow(login).Show();
                    mainWindow.Close();

                }
                else
                {

                    new UserWindow(login).Show();
                    mainWindow.Close();
                }
                
            }
            else
            {
                IncorrectPasswordLable.Visibility = Visibility.Visible; // неправильный логин или пароль
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ForgotPasswordPage(this));
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }


        private void RegAcc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(this));
            IncorrectPasswordLable.Visibility = Visibility.Hidden;
        }
    }
}
