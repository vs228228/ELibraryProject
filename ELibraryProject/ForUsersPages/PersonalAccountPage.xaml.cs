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

namespace ELibraryProject.ForUsersPages
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        CatalogPage catalogPage;
        AboutPage aboutPage;
        string login;
        public PersonalAccountPage(CatalogPage catalogPage, AboutPage aboutPage, string login)
        {
            InitializeComponent();
            this.catalogPage = catalogPage;
            this.aboutPage = aboutPage;
            this.login = login;
        }

        private void ReturnToCatalog(object sender, EventArgs e)
        {
            NavigationService.Navigate(catalogPage);
        }

        private void LoadAboutPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(aboutPage);
        }
    }
}
