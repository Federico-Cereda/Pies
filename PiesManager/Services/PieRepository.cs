using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiesManager.Services
{
    public class PieRepository : IPieRepository
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
        public void Add(Pie pie)
        {
            db.Pies.Add(pie);
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            db.Pies.Remove(db.Pies.Find(id));
            db.SaveChanges();
        }

        public void Update(Pie pie)
        {
            var update = db.Entry(pie);
            update.State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}