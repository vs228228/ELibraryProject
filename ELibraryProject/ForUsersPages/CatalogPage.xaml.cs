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
        ObservableCollection<BookView> books = new ObservableCollection<BookView>();
        CatalogManager catalogManager = new CatalogManager();
        AboutPage aboutPage;
        PersonalAccountPage personalAccountPage;

        public CatalogPage(string login)
        {
            InitializeComponent();
            books = catalogManager.LoadBooks();
            BooksItemsControl.ItemsSource = books;
            aboutPage = new AboutPage(this);
            personalAccountPage = new PersonalAccountPage(this, aboutPage);
            UserContext.CurrentUser = DatabaseHandler.GetUserByLogin(login);

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
    }

}
