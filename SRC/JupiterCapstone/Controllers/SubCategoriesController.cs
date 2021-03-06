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
    [Route("SubCategories")]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategory _repository;
        public SubCategoriesController(ISubCategory repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get-subcategories-bycategoryId")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId([FromQuery] string categoryId)
        {
            if (categoryId==null)
            {
                return BadRequest();
            }
            var subCategories = await _repository.GetSubCategoriesByCategoryIdAsync(categoryId);
            if (subCategories==null)
            {
                return NotFound();
            }
            return Ok(subCategories);
        }

        [HttpGet]
        [Route("get-all-subcategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories =await _repository.GetAllSubCategoriesAsync();
            if (subCategories== null)
            {
                return NotFound();
            }

            return Ok(subCategories);
        }

        [HttpPost]
        [Route("add-subcategories")]
        public async Task<IActionResult> AddSubCategories([FromBody] List<AddSubCategoryDto> addSubCategories)
        {
            var reponse = await _repository.AddSubCategoryAsync(addSubCategories);
            if (!reponse)
            {
                return BadRequest();

            }
          
            return NoContent();

        }
        [HttpPut]
        [Route("update-subcategories")]
        public async Task<IActionResult> UpdateSubCategories([FromBody] List<UpdateSubCategoryDto> updateSubCategories)
        {
           var response=await _repository.UpdateSubCategoryAsync(updateSubCategories);
            if (!response)
            {
                return BadRequest();

            }
            return NoContent();

        }
        [HttpDelete]
        [Route("remove-subcategories")]
        public async Task<IActionResult> DeleteSubCategories(List<string> deleteSubCategories) 
        {
            if (deleteSubCategories.Count==0)
            {
                return BadRequest();

            }
           await _repository.DeleteSubCategoryAsync(deleteSubCategories);
            return NoContent();
        }
       
        
    }
}
