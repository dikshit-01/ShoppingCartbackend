using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
