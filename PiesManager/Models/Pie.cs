using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiesManager.Models
{
    public class Pie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
    }
}