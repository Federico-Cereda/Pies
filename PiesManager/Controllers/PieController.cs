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
            pieRepository.Add(httpRequest);
            return $"Torta aggiunta";
        }

        public string Delete(int id)
        {
            pieRepository.Remove(id);
            return $"Torta eliminata";
        }

        public string Put()
        {
            var httpRequest = HttpContext.Current.Request;
            pieRepository.Update(httpRequest);
            return $"Torta aggiornata";
        }

    }
}
