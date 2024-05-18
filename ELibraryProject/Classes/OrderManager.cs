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
                UserId = UserContext.CurrentUser.Id,
                BookId = book.Id,
                Number = number,
                OrderDate = DateTime.Now
            };
            DatabaseHandler.AddOrder(order);
        }

        public static void ApproveOrder(Order order)
        {
            order.ApprovalDate = DateTime.Now;
            DatabaseHandler.UpdateOrder(order);
        }

        public static void CancelOrder(Order order)
        {
            order.CancellationDate = DateTime.Now;
            DatabaseHandler.UpdateOrder(order);
        }

        public static void CompleteOrder(Order order)
        {
            order.CompletionDate = DateTime.Now;
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
                    UserName = user.FirstName + " " + user.LastName,
                    EmailAddress = user.Email,
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
                    UserName = user.FirstName + " " + user.LastName,
                    EmailAddress = user.Email,
                    Book = book.Title + " " + book.Author,
                    Number = order.Number,
                    Status = "Неподтверждён",
                    StatusDate = order.OrderDate
                });
            }

            return list;
        }

        public static List<OrderView> GetApprovedOrderViewList()
        {
            var list = new List<OrderView>();
            var orders = DatabaseHandler.GetApprovedOrders();
            var books = DatabaseHandler.GetBooks();
            var users = DatabaseHandler.GetUsers();

            foreach (var order in orders)
            {
                var user = users.FirstOrDefault(u => u.Id == order.UserId);
                var book = books.FirstOrDefault(b => b.Id == order.BookId);
                list.Add(new OrderView
                {
                    OrderId = order.Id ?? throw new NullReferenceException(),
                    UserName = user.FirstName + " " + user.LastName,
                    EmailAddress = user.Email,
                    Book = book.Title + " " + book.Author,
                    Number = order.Number,
                    Status = "Подтверждён",
                    StatusDate = order.ApprovalDate ?? throw new NullReferenceException()
                });
            }

            return list;
        }

        public static List<OrderView> GetArchivedOrderViewList()
        {
            var list = new List<OrderView>();
            var orders = DatabaseHandler.GetArchivedOrders();
            var books = DatabaseHandler.GetBooks();
            var users = DatabaseHandler.GetUsers();

            foreach (var order in orders)
            {
                var user = users.FirstOrDefault(u => u.Id == order.UserId);
                var book = books.FirstOrDefault(b => b.Id == order.BookId);
                string status = "";
                DateTime? statusDate = null;
                if (order.CompletionDate != null)
                {
                    status = "Завершён";
                    statusDate = order.CompletionDate;
                }
                else if (order.CancellationDate != null)
                {
                    status = "Отменён";
                    statusDate = order.CancellationDate;
                }
                list.Add(new OrderView
                {
                    OrderId = order.Id ?? throw new NullReferenceException(),
                    UserName = user.FirstName + " " + user.LastName,
                    EmailAddress = user.Email,
                    Book = book.Title + " " + book.Author,
                    Number = order.Number,
                    Status = status,
                    StatusDate = statusDate ?? throw new NullReferenceException()
                });
            }

            return list;
        }
    }
}
