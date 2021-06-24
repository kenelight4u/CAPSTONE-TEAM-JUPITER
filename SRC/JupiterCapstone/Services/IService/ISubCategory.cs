using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
     interface ISubCategory
    {
        IEnumerable<SubCategory> GetAllCategories();//for a user
        void AddSubCategory(List<SubCategory> subcategory);
        void UpdateSubCategory(List<SubCategory> category);
        void DeleteSubCategory(List<SubCategory> subcategory);
        bool SaveChanges();
    }
}
