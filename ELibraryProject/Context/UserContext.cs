using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibraryProject.Database.Models;

namespace ELibraryProject.Context
{
    public static class UserContext
    {
        public static User? CurrentUser { get; set; }
    }
}
