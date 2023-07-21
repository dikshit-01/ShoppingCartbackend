using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Logic_Layer.Services
{
    public class ProductService
    {
        readonly IGenericRepository<Product> _prodRepository;
        readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IGenericRepository<Product> prodRepository,IWebHostEnvironment webHostEnvironment)
        {
            _prodRepository = prodRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async void AddProduct(ProductDTO productDto) {
            Product product = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,

                Rating = productDto.Rating,
            };
            try
            {
                if (productDto.Images?.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = productDto.Images.FileName;
                    using (FileStream fileStream = System.IO.File.Create(path + productDto.Images.FileName))
                    {
                        productDto.Images.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    product.Images = "https://localhost:7241/uploads/" + fileName;
                }
            }
           catch
            {
                product.Images = "noimage";
            }

            _prodRepository.Insert(product);
            _prodRepository.Save();

        }


        public async void EditProd(Guid id,ProductDTO productDto)
        {
            Product product = _prodRepository.FindById(id);

            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;
            product.CategoryId = productDto.CategoryId;
            product.Rating = productDto.Rating;
            product.Name = productDto.Name;

            try
            {
                if (productDto.Images?.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = productDto.Images.FileName;
                    using (FileStream fileStream = System.IO.File.Create(path + productDto.Images.FileName))
                    {
                        productDto.Images.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    product.Images = "https://localhost:7241/uploads/" + fileName;
                }
            }
            catch
            {
                product.Images = "NoImage";
            }


            _prodRepository.Update(product);
            _prodRepository.Save();

        }

        public async Task<ProductPageData> GetProductsToShow(string? sortOrder, string? searchString, int pageNumber, int pageSize = 3)
        {

            var products = await _prodRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchString) || p.Rating.ToLower().Contains(searchString) || p.Description.ToLower().Contains(searchString) || p.Quantity.ToString().ToLower().Contains(searchString) || p.Price.ToString().ToLower().Contains(searchString)).ToList();
            }
              switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "name_asc":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "price_desc":
                    products  = products.OrderByDescending(s => s.Price);
                    break;
                case "price_asc":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "rating_desc":
                    products = products.OrderByDescending(s => s.Rating);
                    break;
                case "rating_asc":
                    products = products.OrderBy(s => s.Rating);
                    break;
                case "quantity_desc":
                    products = products.OrderByDescending(s => s.Quantity);
                    break;
                case "quantity_asc":
                    products = products.OrderBy(s => s.Quantity);
                    break;
                case "description_desc":
                    products = products.OrderByDescending(s => s.Description);
                    break;
                case "description_asc":
                    products = products.OrderBy(s => s.Description);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            int totalPagesTillNow = products.Count();
            var pageResults = (decimal)pageSize;
            var pageCount = Math.Ceiling(products.Count() / pageResults);
            var productsToShow = products.Skip((int)(pageNumber - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToList();
            var response = new ProductResponse()
            {
                Products = productsToShow,
                CurrentPage = pageNumber,
                Pages = (int)pageCount
            };

            ProductPageData productPageData = new ProductPageData()
            {
                ProductsList = productsToShow,
                TotalCount = totalPagesTillNow

            };
            return productPageData;
          
        }


    }
}
