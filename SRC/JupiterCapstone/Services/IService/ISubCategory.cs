using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
     public interface ISubCategory
    {
        IEnumerable<ViewSubCategoryDto> GetAllSubCategories();//for a user
        void AddSubCategory(List<AddSubCategoryDto> addSubcategories);
        void UpdateSubCategory(List<UpdateSubCategoryDto> updateSubcategories);
        void DeleteSubCategory(List<string> deleteSubcategories); 
        void SaveChanges();
    }
}
