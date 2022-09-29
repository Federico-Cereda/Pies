using PiesManager.Models;
using PiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PiesManager.Controllers
{
    public class PieController : ApiController
    {
        private readonly PieRepository pieRepository;

        public PieController(PieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        public IEnumerable<Pie> Get()
        {
            return pieRepository.GetAll();
        }
    }
}
