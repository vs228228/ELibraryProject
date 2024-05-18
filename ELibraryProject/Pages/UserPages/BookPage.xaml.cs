using ELibraryProject.AdminPages.Pages;
using ELibraryProject.Classes;
using ELibraryProject.Context;
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
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        BookView book;
        CatalogPage catalogPage;
        AboutPage aboutPage;

        public BookPage(BookView bookView, CatalogPage catalogPage, AboutPage aboutPage)
        {
            InitializeComponent();
            book = bookView;

            BookTitle.Text = book.Title;
            BookAuthor.Text = book.Author;
            BookCategory.Text = "Категория: " + book.Category;
            BookCoverType.Text = "Обложка: " + book.CoverType;
            BookPublisher.Text = "Издатель: " + book.Publisher;
            BookCategory.Text = "Категория: " + book.Category;
            BookPageCount.Text = "Кол-во страниц: " + book.PageCount;
            BookPublicationDate.Text = "Дата публикации: " + book.PublicationDate;


            BookPrice.Text = "Цена: " + Math.Round(book.Price, 2);
            BookDescription.Text = "Описание: " + book.Description;
            img.Source = book.Picture;
            this.catalogPage = catalogPage;
            this.aboutPage = aboutPage;
            if (UserContext.CurrentUser.IsAdmin is true)
            {
                AboutUsLable.Visibility = Visibility.Hidden;
                PersonalAreaLable.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Hidden;
                OrdersLable.Visibility = Visibility.Visible;
                AddBookLable.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
            }
        }

        private void ReturnToCatalog(object  sender, EventArgs e)
        {
            catalogPage.Update();
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

        private void LoadOrdersPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrdersAdminPage());
        }

        private void LoadAddBookPage(object sender, EventArgs e)
        {
            NavigationService.Navigate(new AddBookPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ConfirmationWindow(book).ShowDialog();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBookPage(book));
        }
    }
}
