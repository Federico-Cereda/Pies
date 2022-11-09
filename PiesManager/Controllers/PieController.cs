using PiesManager.Models;
using PiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PiesManager.Controllers
{
    [EnableCors(origins: "*", headers: "", methods: "")]
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

        public string Post(Pie pie)
        {
            pieRepository.Add(pie);
            return $"Torta {pie.Title} aggiunta";
        }

        public string Delete(int id)
        {
            pieRepository.Remove(id);
            return $"Torta eliminata";
        }

        public string Put(Pie pie)
        {
            pieRepository.Update(pie);
            return $"Torta {pie.Title} aggiornata";
        }

    }
}
