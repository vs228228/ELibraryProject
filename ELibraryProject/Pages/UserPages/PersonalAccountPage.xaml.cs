using ELibraryProject.Classes;
using ELibraryProject.Context;
using ELibraryProject.Database;
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

namespace ELibraryProject.ForUsersPages
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        CatalogPage catalogPage;
        AboutPage aboutPage;
        public PersonalAccountPage(CatalogPage catalogPage, AboutPage aboutPage)
        {
            InitializeComponent();
            this.catalogPage = catalogPage;
            this.aboutPage = aboutPage;
            this.LoginLabel.Content =  $"Ваш логин {UserContext.CurrentUser.Login}";
            OrderTextBlock.Text = LoadOrdersPerUser(); 
        }

        private string LoadOrdersPerUser()
        {
            string str = String.Empty;
            List<OrderView> orders = OrderManager.GetOrderViewList();
            foreach (OrderView order in orders)
            {
                if(order.EmailAddress == UserContext.CurrentUser.Email)
                {
                    str += $"{order.Book} {order.Status}\n";
                }
            }
            return str;
        }

        private void ReturnToCatalog(object sender, EventArgs e)
        {
            NavigationService.Navigate(catalogPage);
        }

        private void LoadAboutPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(aboutPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CatalogManager.OutSystem();
            new MainWindow().Show();
            catalogPage.thisPage.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CatalogManager.OutSystem();
            catalogPage.thisPage.Close();
        }
    }
}
