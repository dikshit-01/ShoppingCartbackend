using Buisness_Logic_Layer.Services;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping_cart_Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        readonly IGenericRepository<Product> _productRepo;
        readonly IGenericRepository<Category> _categoryRepository;
        readonly IGenericRepository<Product> _productRepository;
        readonly IWebHostEnvironment _webHostEnvironment;

        readonly ProductService _productService;
        public ProductController(IGenericRepository<Product> productRepo, ProductService productService, IGenericRepository<Category> categoryRepository, IGenericRepository<Product> productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _productService = productService;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetProducts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await _productRepo.GetAll());
        }


        [HttpGet]
        [Route("GetProductById{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(_productRepository.FindById(id));
        }


        [HttpGet]
        [Route("GetCategories")]
        public async Task<ActionResult> GetAllCategories()
        {
            return Ok(await _categoryRepository.GetAll());
        }

        [HttpPost]
        [Route("PostProducts")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostProduct([FromForm] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("EditProducts{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProduct([FromRoute] Guid id, [FromForm] ProductDTO productDTO)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _productService.EditProd(id, productDTO);
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteProducts{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(product);
            _productRepository.Save();
            return Ok(product);
        }



        [HttpGet("getProductsToShow")]
        public async Task<ActionResult> GetProductsToShow(string? sortOrder, string? searchString, int pageNumber, int pageSize=3)
        {
            var products = await _productRepository.GetAll();
            ProductPageData productPageData = await _productService.GetProductsToShow(sortOrder, searchString, pageNumber, pageSize);
            if (productPageData == null) return BadRequest();
            return Ok(productPageData);
        }







    }
}
