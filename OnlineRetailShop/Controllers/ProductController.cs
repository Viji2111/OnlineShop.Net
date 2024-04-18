using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.BusinessLogicLayer.Interfaces;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;

        public ProductController(IProductService productService)
        {
            _productservice = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productservice.GetProduct();
            return Ok(products);
        }

        [HttpGet]
        [Route("/GetProductById")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            var product = await _productservice.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("AddProduct")]


        public async Task<IActionResult> PostProduct(Product product)
        {
            try
            {
                Product newProduct = await _productservice.PostProduct(product);
                return Ok(GetProductById(newProduct.ProductId));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var result = await _productservice.PutProduct(product);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productservice.DeleteProduct(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}

