using Microsoft.Ajax.Utilities;
using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public Pie Get(int id)
        {
            return db.Pies.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
        public void Add(Pie pie)
        {
            db.Pies.Add(pie);
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            var image = db.Pies.FirstOrDefault(p => p.Id == id).Image;
            db.Pies.Remove(db.Pies.Find(id));
            db.SaveChanges();
            if (db.Pies.Where(p => p.Image == image).FirstOrDefault() is null)
            {
                var path = Path.GetFileName(image);
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/" + path);
                File.Delete(imagePath);
            }
        }

        public void Update(Pie pie)
        {
            var image = db.Pies.AsNoTracking().FirstOrDefault(p => p.Id == pie.Id).Image;
            var update = db.Entry(pie);
            update.State = EntityState.Modified;
            db.SaveChanges();
            if (db.Pies.Where(p => p.Image == image).FirstOrDefault() is null)
            {
                var path = Path.GetFileName(image);
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/" + path);
                File.Delete(imagePath);
            }
            
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }
        }
    }
}