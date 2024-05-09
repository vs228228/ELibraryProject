using ELibraryProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    internal class OrderManager
    {
        public static void CreateOrder(Book book, int number)
        {
            var order = new Order
            {
                UserId = 5,
                BookId = book.Id,
                Number = number,
                OrderDate = DateTime.Now,
                IsComplete = false
            };
            DatabaseHandler.AddOrder(order);
        }

        public static void ApproveOrder(Order order)
        {
            order.ApprovalDate = DateTime.Now;
            DatabaseHandler.UpdateOrder(order);
        }

        public static List<OrderView> GetOrderViewList()
        {
            var list = new List<OrderView>();
            var orders = DatabaseHandler.GetOrders();
            var books = DatabaseHandler.GetBooks();
            var users = DatabaseHandler.GetUsers();

            foreach (var order in orders )
            {
                var user = users.FirstOrDefault(u => u.Id == order.UserId);
                var book = books.FirstOrDefault(b => b.Id == order.BookId);
                list.Add(new OrderView
                {
                    OrderId = (int)order.Id,
                    User = user.FirstName + " " + user.LastName,
                    Book = book.Title + " " + book.Author,
                    Number = order.Number
                });
            }

            return list;
        }

        public static List<OrderView> GetUnapprovedOrderViewList()
        {
            var list = new List<OrderView>();
            var orders = DatabaseHandler.GetUnapprovedOrders();
            var books = DatabaseHandler.GetBooks();
            var users = DatabaseHandler.GetUsers();

            foreach (var order in orders)
            {
                var user = users.FirstOrDefault(u => u.Id == order.UserId);
                var book = books.FirstOrDefault(b => b.Id == order.BookId);
                list.Add(new OrderView
                {
                    OrderId = (int)order.Id,
                    User = user.FirstName + " " + user.LastName,
                    Book = book.Title + " " + book.Author,
                    Number = order.Number
                });
            }

            return list;
        }
    }
}
