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
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        BookView book;
        CatalogPage catalogPage;
        AboutPage aboutPage;

        public BookPage(string TitleAndAuthor, CatalogPage catalogPage, AboutPage aboutPage)
        {
            InitializeComponent();
            book = CatalogManager.GetCertainBook(TitleAndAuthor);
            BookTitle.Text = book.Title;
            BookAuthor.Text = book.Author;
            BookPrice.Text = "Цена: " + Math.Round(book.Price, 2);
            BookDescription.Text = "Описание: " + book.Description;
            //    img.Source = Image.FromFile("C:\\Users\\Ваня\\Desktop\\nastol.com.ua-9967.jpg", "");
            img.Source = new BitmapImage(new Uri(book.PicturePath));
            this.catalogPage = catalogPage;
            this.aboutPage = aboutPage;
          //  MessageBox.Show(book.Count.ToString());
        }

        private void ReturnToCatalog(object  sender, EventArgs e)
        {
            NavigationService.Navigate(catalogPage);
        }

        private void LoadAboutPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(aboutPage);
        }

        private void LoadPersonalAccountPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new PersonalAccountPage(catalogPage, aboutPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ConfirmationWindow(book).ShowDialog();
        }
    }
}
