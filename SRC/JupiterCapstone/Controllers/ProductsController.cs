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
       public IActionResult GetAllProducts()
       {
            var allProducts = _repository.GetAllProducts().ToList();
            if (allProducts.Count==0)
            {
                return NotFound();
            }
           
            return Ok(allProducts);
       }

        [HttpPost]
        [Route("add")]
        public IActionResult AddProducts([FromBody] List<AddProductDto> addProduct)
        {
            _repository.AddProduct(addProduct);
            return NoContent();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult DeleteProducts([FromBody] List<UpdateProductDto> productToUpdate)
        {
            _repository.UpdateProduct(productToUpdate);
            return NoContent();
        }


        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteProducts([FromBody]List<string>productsToDelete)
        {
            _repository.DeleteProduct(productsToDelete);
            return NoContent();
        }

        [HttpPost]
        [Route("search")]
        public IActionResult GetProductsByName([FromBody] List<string> productsName)
        {
           var products= _repository.GetProductsByName(productsName);
            if (products==null)
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}
