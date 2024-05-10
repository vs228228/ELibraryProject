using ELibraryProject.AdminPages.Pages;
using ELibraryProject.Classes;
using ELibraryProject.Database;
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
        private ObservableCollection<BookView> searchedBooks;
        private AboutPage aboutPage;
        private PersonalAccountPage personalAccountPage;

        public CatalogPage(string login)
        {

            InitializeComponent();
            allBooks = CatalogManager.LoadBooks();
            BooksItemsControl.ItemsSource = allBooks;
            UserContext.CurrentUser = DatabaseHandler.GetUserByLogin(login);
            aboutPage = new AboutPage(this);
            personalAccountPage = new PersonalAccountPage(this, aboutPage);
            if(UserContext.CurrentUser.IsAdmin is true)
            {
                AboutUsLable.Visibility = Visibility.Hidden;
                PersonalAreaLable.Visibility = Visibility.Hidden;
                OrdersLable.Visibility = Visibility.Visible;
                AddBookLable.Visibility = Visibility.Visible;
            }
            

        }

        private void LoadBookPage(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Ищем TextBlock внутри кнопки по имени
            TextBlock textBlock = clickedButton.FindName("InfoTextBlock") as TextBlock;

            if (textBlock != null)
            {
                // Теперь есть доступ к TextBlock и его свойствам
                string text = textBlock.Text;
                NavigationService.Navigate(new BookPage(text, this,this.aboutPage));
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

       }
    }

