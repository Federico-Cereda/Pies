using Antlr.Runtime;
using PiesManager.Models;
using PiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PiesManager.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PieController : ApiController
    {
        private readonly IPieRepository pieRepository;

        public PieController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        public IEnumerable<Pie> Get()
        {
            return pieRepository.GetAll();
        }


        public string Post()
        {
            var httpRequest = HttpContext.Current.Request;

            var title = httpRequest.Params["Title"];
            var price = httpRequest.Params["Price"];
            var desc = httpRequest.Params["Desc"];
            var storePathToDb = string.Empty;

            if (httpRequest.Files.Count > 0)
            {   
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                    var fileHttp = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api/Pie", "Images/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    storePathToDb = fileHttp;
                }
            }

            var pie = new Pie { Title = title, Price = int.Parse(price), Desc = desc, Image = storePathToDb };
            pieRepository.Add(pie);
            return $"Torta {pie.Title} aggiunta";
        }

        public string Delete(int id)
        {
            pieRepository.Remove(id);
            return $"Torta eliminata";
        }

        public string Put()
        {
            var httpRequest = HttpContext.Current.Request;

            var id = httpRequest.Params["Id"];
            var title = httpRequest.Params["Title"];
            var price = httpRequest.Params["Price"];
            var desc = httpRequest.Params["Desc"];
            var storePathToDb = string.Empty;

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api/Pie", "Images/" + postedFile.FileName);
                    storePathToDb = filePath;
                }
            }

            if (storePathToDb == "")
            {
                var pre = pieRepository.Get(int.Parse(id)).Image;
                storePathToDb = pre;
            }
            var pie = new Pie { Id = int.Parse(id), Title = title, Price = int.Parse(price), Desc = desc, Image = storePathToDb };
            pieRepository.Update(pie);
            return $"Torta {pie.Title} aggiornata";
        }

    }
}
