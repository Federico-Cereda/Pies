using Microsoft.Ajax.Utilities;
using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
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
            //return from p in db.Pies
            //       orderby p.Id
            //       select p;

            var listPies = new List<Pie>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Pies ORDER BY Id", conn);
                // Create a new SqlDataReader object and read data from the command.    
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //while there is another record present
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Pie pie = new Pie
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetInt32(reader.GetOrdinal("Price")),
                                Desc= reader.GetString(reader.GetOrdinal("Desc")),
                                Image= reader.GetString(reader.GetOrdinal("Image"))
                            };
                            listPies.Add(pie);
                        }
                    }
                }
            }
            return listPies;
        }

        public Pie Get(int id)
        {
            //return db.Pies.AsNoTracking().FirstOrDefault(p => p.Id == id);

            var pie = new Pie();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP (1) * FROM Pies WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            pie = new Pie
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetInt32(reader.GetOrdinal("Price")),
                                Desc = reader.GetString(reader.GetOrdinal("Desc")),
                                Image = reader.GetString(reader.GetOrdinal("Image"))
                            };
                        }
                    }
                }
            }
            return pie;
        }

        public void Add(Pie pie)
        {
            //db.Pies.Add(pie);
            //db.SaveChanges();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Pies ([Title], [Price], [Desc], [Image]) VALUES (@Title, @Price, @Desc, @Image)", conn);
                command.Parameters.Add(new SqlParameter("Title", pie.Title));
                command.Parameters.Add(new SqlParameter("Price", pie.Price));
                command.Parameters.Add(new SqlParameter("Desc", pie.Desc));
                command.Parameters.Add(new SqlParameter("Image", pie.Image));
                command.ExecuteNonQuery();
            } 
        }

        public void Remove(int id)
        {
            //image = db.Pies.FirstOrDefault(p => p.Id == id).Image;

            var image = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM Pies WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            image = reader.GetString(0);
                        }
                    }
                }
            }

            

            //db.Pies.Remove(db.Pies.Find(id));
            //db.SaveChanges();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Pies WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            }

            

            var picture = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP (1) Image FROM Pies WHERE [Image] = @Image", conn);
                command.Parameters.Add(new SqlParameter("Image", image));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            picture = reader.GetString(0);
                        }
                    }
                }
            }
            if (picture == "")
            {
                var path = Path.GetFileName(image);
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/" + path);
                File.Delete(imagePath);
            }
        }

        public void Update(Pie pie)
        {
            //var image = db.Pies.AsNoTracking().FirstOrDefault(p => p.Id == pie.Id).Image;

            var image = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT Image FROM Pies WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", pie.Id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            image = reader.GetString(0);
                        }
                    }
                }
            }

            

            //var update = db.Entry(pie);
            //update.State = EntityState.Modified;
            //db.SaveChanges();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("UPDATE Pies SET [Title] = @Title, [Price] = @Price, [Desc] = @Desc, [Image] = @Image WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", pie.Id));
                command.Parameters.Add(new SqlParameter("Title", pie.Title));
                command.Parameters.Add(new SqlParameter("Price", pie.Price));
                command.Parameters.Add(new SqlParameter("Desc", pie.Desc));
                command.Parameters.Add(new SqlParameter("Image", pie.Image));
                command.ExecuteNonQuery();
            }

            

            var picture = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP (1) Image FROM Pies WHERE [Image] = @Image", conn);
                command.Parameters.Add(new SqlParameter("Image", image));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            picture = reader.GetString(0);
                        }
                    }
                }
            }
            if (picture == "")
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