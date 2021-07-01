using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.DTO.Admin;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface ISubCategory
    {
        //Task<IEnumerable<ViewSubCategoryDto>> GetAllSubCategoriesAsync();//for a user
        Task<bool> AddSubCategoryAsync(List<AddSubCategoryDto> addSubcategories);
        Task<bool> UpdateSubCategoryAsync(List<UpdateSubCategoryDto> updateSubcategories);
        Task DeleteSubCategoryAsync(List<string> deleteSubcategories); 
        Task<IEnumerable<ViewSubCategoryDto>> GetSubCategoriesByCategoryIdAsync(string userId);
    }
}
