using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
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
        [Route("subcategoryid")]
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
        /*[HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _repository.GetAllProductsAsync();
            if (allProducts == null)
            {
                return NotFound();
            }

            return Ok(allProducts);
        }*/

        [HttpPost]
        [Route("add")]
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
        [Route("update")]
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
        [Route("delete")]
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
        [Route("search")]
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
