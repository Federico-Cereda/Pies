using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PiesManager.Services
{
    public class PiesDbContext : DbContext
    {
        public PiesDbContext() : base("PiesDbContext")
        {
        }

        public DbSet<Pie> Pies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}