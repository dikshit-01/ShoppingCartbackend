using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using ClosedXML.Excel;
using NPOI.SS.Formula.Functions;
using NPOI.HPSF;
using System.IO;
using ExcelDataReader;
using System.IO.Packaging;
using OfficeOpenXml;

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

        public async Task<IEnumerable<Product>> ImportData(IFormFile file)
        {
            
            string folderName = "ExcelSheets";
            string webRootPAth = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPAth, folderName);
            StringBuilder sb = new StringBuilder();

            if (!Directory.Exists(newPath)) {
            
                Directory.CreateDirectory(newPath);
            }
            if(file.Length>0)
            {
               
               using(var stream = new MemoryStream())
                {
                  await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++) // Assuming the first row contains headers
                        {
                            var name = worksheet.Cells[row, 1].Value?.ToString();
                            var description = worksheet.Cells[row, 2].Value?.ToString();
                            var images = worksheet.Cells[row, 3].Value?.ToString();
                            var quantity = Convert.ToInt32(worksheet.Cells[row, 4].Value);
                            float price = float.Parse(worksheet.Cells[row, 5].Value.ToString());
                            var rating = worksheet.Cells[row, 6].Value.ToString();
                            var categoryID = Convert.ToInt32(worksheet.Cells[row, 7].Value);

                            var product = new Product
                            {
                                Name = name,
                                Description = description,
                                Images = images,
                                Quantity = quantity,
                                Price = price,
                                Rating = rating,
                                CategoryId = categoryID
                            };

                            _prodRepository.Insert(product);
                            _prodRepository.Save();
                        }
                    }

                }
            }
            var newProductList = await _prodRepository.GetAll();
            return newProductList;
        }
    
        public async Task<byte[]> ExportToExcel()
        {
            var productList = await _prodRepository.GetAll();
            using (var workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                workSheet.Cell(currentRow, 1).Value = "Name";
                workSheet.Cell(currentRow, 2).Value = "Description";
                workSheet.Cell(currentRow, 3).Value = "Images";
                workSheet.Cell(currentRow, 4).Value = "Quantity";
                workSheet.Cell(currentRow, 5).Value = "Price";
                workSheet.Cell(currentRow, 6).Value = "Rating";
                workSheet.Cell(currentRow, 7).Value = "CategoryId";

                foreach (var product in productList)
                {
                    currentRow++;
                    workSheet.Cell(currentRow,1).Value = product.Name;
                    workSheet.Cell(currentRow, 2).Value = product.Description;
                    workSheet.Cell(currentRow, 3).Value = product.Images;
                    workSheet.Cell(currentRow, 4).Value = product.Quantity;
                    workSheet.Cell(currentRow, 5).Value = product.Price;
                    workSheet.Cell(currentRow, 6).Value = product.Rating;
                    workSheet.Cell(currentRow, 7).Value = product.CategoryId;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }

            }
            
           
        }
    }
}
