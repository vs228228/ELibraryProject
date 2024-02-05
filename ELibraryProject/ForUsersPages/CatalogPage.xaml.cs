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
        ObservableCollection<DoubleBook> books = new ObservableCollection<DoubleBook>();
        CatalogeManager catalogeManager = new CatalogeManager();

        public CatalogPage()
        {
            InitializeComponent();
            books = catalogeManager.LoadBooks();
            Books1ListBox.ItemsSource = books;
            
        }

    }

}
