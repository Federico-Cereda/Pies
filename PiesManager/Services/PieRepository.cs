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
            var listPies = new List<Pie>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Pies ORDER BY Id", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
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

        public void Add(HttpRequest httpRequest)
        {
            var title = httpRequest.Params["Title"];
            var price = httpRequest.Params["Price"];
            var desc = httpRequest.Params["Desc"];
            var image = string.Empty;

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var fileHttp = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api/Pie", "Images/" + postedFile.FileName);
                    image = fileHttp;
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Pies ([Title], [Price], [Desc], [Image]) VALUES (@Title, @Price, @Desc, @Image)", conn);
                command.Parameters.Add(new SqlParameter("Title", title));
                command.Parameters.Add(new SqlParameter("Price", price));
                command.Parameters.Add(new SqlParameter("Desc", desc));
                command.Parameters.Add(new SqlParameter("Image", image));
                command.ExecuteNonQuery();
            } 
        }

        public void Remove(int id)
        {
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

        public void Update(HttpRequest httpRequest)
        {
            var id = httpRequest.Params["Id"];
            var title = httpRequest.Params["Title"];
            var price = httpRequest.Params["Price"];
            var desc = httpRequest.Params["Desc"];
            var image = string.Empty;

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var fileHttp = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api/Pie", "Images/" + postedFile.FileName);
                    image = fileHttp;
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }

            var imago = string.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP (1) Image FROM Pies WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            imago = reader.GetString(0);
                            if (image == "")
                            {
                                image = imago;
                            }
                        }
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("UPDATE Pies SET [Title] = @Title, [Price] = @Price, [Desc] = @Desc, [Image] = @Image WHERE [Id] = @Id", conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                command.Parameters.Add(new SqlParameter("Title", title));
                command.Parameters.Add(new SqlParameter("Price", price));
                command.Parameters.Add(new SqlParameter("Desc", desc));
                command.Parameters.Add(new SqlParameter("Image", image));
                command.ExecuteNonQuery();
            }

            var picture = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["PiesDbContext"].ToString();
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP (1) Image FROM Pies WHERE [Image] = @Image", conn);
                command.Parameters.Add(new SqlParameter("Image", imago));
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
                var path = Path.GetFileName(imago);
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/" + path);
                File.Delete(imagePath);
            }
        }
    }
}