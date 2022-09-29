using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiesManager.Services
{
    public class PiesDbContext : DbContext
    {
        public DbSet<Pie> Pies { get; set; }
    }
}