using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { CategoryId=1,CategoryName="Electronics"},
                new Category { CategoryId=2,CategoryName="Clothings"},
                new Category { CategoryId=3,CategoryName="Grocery"},
                new Category { CategoryId=4,CategoryName="Confectionaries"}
                );
        }
    }
}
