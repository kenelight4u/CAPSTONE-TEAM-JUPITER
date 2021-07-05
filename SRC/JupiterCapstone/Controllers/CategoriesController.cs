using JupiterCapstone.DTO.Admin;
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
        [Route("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            if (categories==null)
            {
                return NotFound();
            } 

            return Ok(categories);
        }

        [HttpPost]
        [Route("add-categories")]
        public async Task<IActionResult> AddCategories([FromBody] List<AddCategoryDto> addCategories)
        {
            var response=await _repository.AddCategoryAsync(addCategories);
            if (!response)
            {
                return BadRequest();
            }
            return NoContent();

        }

        [HttpPut]
        [Route("update-categories")]
        public async Task<IActionResult> UpdateCategories([FromBody] List<UpdateCategoryDto> updateCategories)
        {
            var response = await _repository.UpdateCategoryAsync(updateCategories);
            if (!response)
            {
                return BadRequest();
            } 
            return NoContent();

        }

        [HttpDelete]
        [Route("remove-categories")]
        public async Task<IActionResult> DeleteCategories(List<string> deleteCategories)
        {
            if (deleteCategories.Count==0)
            {
                return BadRequest();
            }
            await _repository.DeleteCategoryAsync(deleteCategories);
            return NoContent();
        }

    }

}
