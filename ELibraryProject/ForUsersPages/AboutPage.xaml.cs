using ELibraryProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Логика взаимодействия для AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        CatalogPage catalogPage;
        string login;
        public AboutPage( CatalogPage catalogPage, string login)
        {
            InitializeComponent();
            this.catalogPage = catalogPage;
            this.login = login;
        }

        private void ReturnToCatalog(object sender, EventArgs e)
        {
            NavigationService.Navigate(catalogPage);
        }

        private void LoadPersonalAccountPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new PersonalAccountPage(catalogPage, this, login));
        }
    }
}
