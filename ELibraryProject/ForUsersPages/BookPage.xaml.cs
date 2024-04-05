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
        CatalogManager manager = new CatalogManager();
        Book book;
        CatalogPage catalogPage;
        public BookPage(string TitleAndAuthor, CatalogPage catalogPage)
        {
            InitializeComponent();
            book = manager.GetCertainBook(TitleAndAuthor);
            BookTitle.Text = book.Title;
            BookAuthor.Text = book.Author;
            BookPrice.Text = "Цена: " + Math.Round(book.Price, 2);
            // BookDescription.Text = "Описание: " + book.Description;

            this.catalogPage = catalogPage;
        }

        private void ReturnToCatalog(object  sender, EventArgs e)
        {
            NavigationService.Navigate(catalogPage);
        }
    }
}
