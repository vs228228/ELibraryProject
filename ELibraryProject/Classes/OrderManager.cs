using ELibraryProject.Databases;
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
                OrderDate = DateTime.Now,
                IsComplete = false
            };
            DatabaseHandler.AddOrder(order);
        }

        public static void ApproveOrder(Order order)
        {
            order.ApprovalDate = DateTime.Now;

        }
    }
}
