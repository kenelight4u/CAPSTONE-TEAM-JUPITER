using JupiterCapstone.DTO.Admin;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly IProduct _repository;
        public ProductsController(IProduct repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get-products-bysubcategoryid")]
        public async Task<IActionResult> GetProductsBySubCategoryId([FromQuery] string subcategoryId)
        {
            if (subcategoryId == null)
            {
                return BadRequest();
            }
            var products = await _repository.GetProductsBySubCategoryIdAsync(subcategoryId);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _repository.GetAllProductsAsync();
            if (allProducts == null)
            {
                return NotFound();
            }

            return Ok(allProducts); 
        }

        [HttpGet]
        [Route("get-product-byid")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            if (productId==null)
            {
                return NotFound();
            }
            var product = await _repository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("add-products")]
        public async Task<IActionResult> AddProducts([FromBody] List<AddProductDto> addProduct)
        {
            var reponse= await _repository.AddProductAsync(addProduct);
            if (!reponse)
            {
                return BadRequest();

            }
            return NoContent();
        }

        [HttpPut]
        [Route("update-products")]
        public async Task<IActionResult> UpdateProducts([FromBody] List<UpdateProductDto> productToUpdate)
        {
            var response=await _repository.UpdateProductAsync(productToUpdate);
            if (!response)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-products")]
        public async Task<IActionResult> DeleteProducts([FromBody]List<string>productsToDelete)
        {
            if (productsToDelete.Count==0) 
            {
                return BadRequest();
            }
            await _repository.DeleteProductAsync(productsToDelete);
            return NoContent();
        }

        [HttpPost]
        [Route("search-for-products-byname")]
        public async Task <IActionResult> GetProductsByName([FromBody] List<string> productsName)
        {
            if (productsName.Count==0)
            {
                return BadRequest();
            }
            else
            {
                var products = await _repository.GetProductsByNameAsync(productsName);
                if (products == null)
                {
                    return NotFound();
                }
                return Ok(products);

            }
          
        }
       
    }
    
}
