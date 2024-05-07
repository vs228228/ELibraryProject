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
using ELibraryProject.Classes;
using ELibraryProject.Database;

namespace ELibraryProject.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для OrdersAdminPage.xaml
    /// </summary>
    public partial class OrdersAdminPage : Page
    {
        private ObservableCollection<OrderView> orders;
        public OrdersAdminPage()
        {
            InitializeComponent();

            orders = new ObservableCollection<OrderView>(OrderManager.GetUnapprovedOrderViewList());
            unapprovedOrdersItemsControl.ItemsSource = orders;
        }
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                OrderView orderView = button.CommandParameter as OrderView;
                if (orderView != null)
                {
                    var order = DatabaseHandler.GetOrderById(orderView.OrderId);
                    OrderManager.ApproveOrder(DatabaseHandler.GetOrderById(orderView.OrderId));
                }
            }
        }
    }
}
