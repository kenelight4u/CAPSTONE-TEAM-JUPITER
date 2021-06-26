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
    [Route("SubCategories")]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategory _repository;
        public SubCategoriesController(ISubCategory repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllSubCategories()
        {
            var subCategories = _repository.GetAllSubCategories().ToList();
            if (subCategories.Count == 0)
            {
                return NotFound();
            }

            return Ok(subCategories);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddSubCategories([FromBody] List<AddSubCategoryDto> addSubCategories)
        {
            _repository.AddSubCategory(addSubCategories);
            return NoContent();

        }
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSubCategories([FromBody] List<UpdateSubCategoryDto> updateSubCategories)
        {
            _repository.UpdateSubCategory(updateSubCategories);
            return NoContent();

        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteSubCategories(List<string> deleteSubCategories) 
        {
            _repository.DeleteSubCategory(deleteSubCategories);
            return NoContent();
        }
        
    }
}
