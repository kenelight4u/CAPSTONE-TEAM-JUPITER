using JupiterCapstone.DTO.Admin;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface ICategory
    {
        Task<IEnumerable<ViewCategoryDto>> GetAllCategoriesAsync();
        Task<bool> AddCategoryAsync(List<AddCategoryDto> categoriesToAdd);
        Task<bool> UpdateCategoryAsync(List<UpdateCategoryDto> categoriesToUpdate); 
        Task DeleteCategoryAsync(List<string> categoriesToDelete);
       
    }
}
