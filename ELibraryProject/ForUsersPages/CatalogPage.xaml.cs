using ELibraryProject.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для UserMainPage.xaml
    /// </summary>
    /// 
    public partial class CatalogPage : Page
    {
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        CatalogManager catalogManager = new CatalogManager();

        public CatalogPage()
        {
            InitializeComponent();
            books = catalogManager.LoadBooks();
            BooksItemsControl.ItemsSource = books;
            
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
                NavigationService.Navigate(new BookPage(text, this));
            }
        }
    }

}
