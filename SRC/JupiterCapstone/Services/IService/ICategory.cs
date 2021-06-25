using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface ICategory
    {
        IEnumerable<ViewCategoryDto> GetAllCategories();//for a user
        void AddCategory(List<AddCategoryDto> categoriesToAdd);
        void UpdateCategory(List<UpdateCategoryDto> categoriesToUpdate); 
        void DeleteCategory(List<string> categoriesToDelete);
        void SaveChanges();
    }
}
