using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PiesManager.Services
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAll();
        void Add(HttpRequest httpRequest);
        void Remove(int id);
        void Update(HttpRequest httpRequest);
    }
}
