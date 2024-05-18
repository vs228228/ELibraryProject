using ELibraryProject.AdminPages.Pages;
using ELibraryProject.Classes;
using ELibraryProject.Context;
using ELibraryProject.Database;
using ELibraryProject.Managers;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace ELibraryProject.ForUsersPages
{
    /// <summary>
    /// Логика взаимодействия для UserMainPage.xaml
    /// </summary>
    /// 
    public partial class CatalogPage : Page
    {
        private ObservableCollection<BookView> allBooks;
        private AboutPage aboutPage;
        private PersonalAccountPage personalAccountPage;
        public Window thisPage;

        public CatalogPage(string login, Window thisPage)
        {
            InitializeComponent();
            UserContext.CurrentUser = DatabaseHandler.GetUserByLogin(login);
            Update();
            aboutPage = new AboutPage(this);
            personalAccountPage = new PersonalAccountPage(this, aboutPage);
            if (UserContext.CurrentUser.IsAdmin is true)
            {
                AboutUsLable.Visibility = Visibility.Hidden;
                PersonalAreaLable.Visibility = Visibility.Hidden;
                OrdersLable.Visibility = Visibility.Visible;
                AddBookLable.Visibility = Visibility.Visible;
            }
            this.thisPage = thisPage;
        }

        private void LoadBookPage(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            BookView bookView = button.CommandParameter as BookView;
            BookView book = allBooks.SingleOrDefault(book => book.Id == bookView.Id);
            if (book != null)
            {
                NavigationService.Navigate(new BookPage(book, this, this.aboutPage));
            }
        }

        private void LoadAboutPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(aboutPage);
        }

        private void LoadPersonalAccountPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new PersonalAccountPage(this, aboutPage));
        }
        private void LoadCatalogPage(object sender, EventArgs e)
        {
            Update();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text;
            BooksItemsControl.ItemsSource = new ObservableCollection<BookView>(allBooks.Where(p => p.TitleAndAuthor.Contains(searchText)));
        }
    
        private void LoadOrdersPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrdersAdminPage());
        }

        private void LoadAddBookPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new AddBookPage());
        }

        public void Update()
        {
            allBooks = CatalogManager.LoadBooks();
            BooksItemsControl.ItemsSource = allBooks;
        }
    }
}

