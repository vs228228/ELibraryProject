using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    internal class Order
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Number { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
