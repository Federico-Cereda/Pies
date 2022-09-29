using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiesManager.Services
{
    public class PieRepository
    {
        private readonly PiesDbContext db;

        public PieRepository(PiesDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Pie> GetAll()
        {
            return from p in db.Pies
                   orderby p.Id
                   select p;
        }

    }
}