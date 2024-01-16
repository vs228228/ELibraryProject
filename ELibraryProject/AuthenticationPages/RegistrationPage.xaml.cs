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

namespace ELibraryProject
{
    delegate bool TryToReg(string name, string secondName, string email, string login, string password, string passwordAgain,
        string codeWord, string tipToCodeWord, out string msg);

    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        LoginPage loginPage;

        public RegistrationPage(LoginPage loginPage)
        {
            InitializeComponent();
            this.loginPage = loginPage;
        }

        private TryToReg? tryToReg = null; // поменять потом на метод, который регистрирует

        private void BackLable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(loginPage);
        }

        private void RebButton_Click(object sender, RoutedEventArgs e)
        {
            string name = FirstNameTextBox.Text;
            string secondName = SecondNameTextBox.Text;
            string email = EmailTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            string passwordAgain = PasswordAgainTextBox.Text;
            string codeWord = CodeWordTextBox.Text;
            string tipToCodeWord = TipToCodeWordTextBox.Text;


            if(tryToReg != null &&
                tryToReg(name, secondName, email, login, password, passwordAgain, codeWord, tipToCodeWord, out string msg) is true)
            {
                // сделать возвращение к прошлому окну и успешную регистрацию
            }
            else
            {
                // написать сообщение об  регистрацию неуспешной. msg вернёт сообщение с первой ошибкой
            }
        }
    }
}
