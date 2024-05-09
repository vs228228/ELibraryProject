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
        private ObservableCollection<OrderView> unapprovedOrders;
        private ObservableCollection<OrderView> approvedOrders;
        private ObservableCollection<OrderView> archivedOrders;
        public OrdersAdminPage()
        {
            InitializeComponent();

            unapprovedOrders = new ObservableCollection<OrderView>(OrderManager.GetUnapprovedOrderViewList());
            unapprovedOrdersItemsControl.ItemsSource = unapprovedOrders;

            approvedOrders = new ObservableCollection<OrderView>(OrderManager.GetApprovedOrderViewList());
            approvedOrdersItemsControl.ItemsSource = approvedOrders;

            archivedOrders = new ObservableCollection<OrderView>(OrderManager.GetArchivedOrderViewList());
            archivedOrdersItemsControl.ItemsSource = archivedOrders;
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                OrderView orderView = button.CommandParameter as OrderView;
                if (orderView != null)
                {
                    OrderManager.ApproveOrder(DatabaseHandler.GetOrderById(orderView.OrderId));
                    orderView.Status = "Подтверждён";
                    orderView.StatusDate = DateTime.Now;
                    approvedOrders.Add(orderView);
                    unapprovedOrders.Remove(orderView);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                OrderView orderView = button.CommandParameter as OrderView;
                if (orderView != null)
                {
                    OrderManager.CancelOrder(DatabaseHandler.GetOrderById(orderView.OrderId));
                    orderView.Status = "Отменён";
                    orderView.StatusDate = DateTime.Now;
                    archivedOrders.Add(orderView);
                    approvedOrders.Remove(orderView);
                    unapprovedOrders.Remove(orderView);
                }
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                OrderView orderView = button.CommandParameter as OrderView;
                if (orderView != null)
                {
                    OrderManager.CompleteOrder(DatabaseHandler.GetOrderById(orderView.OrderId));
                    orderView.Status = "Завершён";
                    orderView.StatusDate = DateTime.Now;
                    archivedOrders.Add(orderView);
                    approvedOrders.Remove(orderView);
                }
            }
        }
    }
}
