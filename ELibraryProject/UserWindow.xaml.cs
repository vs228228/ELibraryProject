// using ELibraryProject.PersonalAccounts;
using ELibraryProject.ForUsersPages;
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

namespace ELibraryProject
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        public UserWindow(string login)
        {
            InitializeComponent();
            this.MaxHeight = 810;
            this.MaxWidth = 860;
            this.MinHeight = 810;
            this.MinWidth = 860;
            UserFrame.Content = new CatalogPage(login, this);
        }
    }
}
