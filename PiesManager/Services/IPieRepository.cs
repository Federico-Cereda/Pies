using PiesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiesManager.Services
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAll();
        Pie Get(int id);
        void Add(Pie pie);
        void Remove(int id);
        void Update(Pie pie);
    }
}
