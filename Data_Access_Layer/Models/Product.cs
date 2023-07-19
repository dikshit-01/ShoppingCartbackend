using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Product
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Required]
        public string Rating { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public string? Images { get; set; } 

        public Category Category { get; set; }
    }
}
