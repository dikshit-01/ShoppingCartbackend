using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ProductPageData
    {
        public List<Product> ProductsList { get; set; }
        public int TotalCount { get; set; }
    }
}
