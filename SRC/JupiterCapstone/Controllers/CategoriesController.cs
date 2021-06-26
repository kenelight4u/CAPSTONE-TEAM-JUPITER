using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoriesController : Controller
    {
       private readonly ICategory _repository;
       public CategoriesController(ICategory repository)
       {
            _repository = repository;
       }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _repository.GetAllCategories().ToList();
            if (categories.Count==0)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCategories([FromBody] List<AddCategoryDto> addCategories)
        {
            _repository.AddCategory(addCategories);
            return NoContent();

        }
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCategories([FromBody] List<UpdateCategoryDto> updateCategories)
        {
            _repository.UpdateCategory(updateCategories);
            return NoContent();

        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCategories(List<string> deleteCategories)
        {
            _repository.DeleteCategory(deleteCategories);
            return NoContent();
        }
    }

}
