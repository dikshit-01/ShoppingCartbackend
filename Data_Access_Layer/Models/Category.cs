using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
     
        public string CategoryName { get; set; }
       

        public ICollection<Product> Products { get; set; }
    }
}
