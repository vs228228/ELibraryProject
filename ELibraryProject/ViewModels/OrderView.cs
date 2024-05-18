using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryProject.Classes
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Book { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }

    }
}
