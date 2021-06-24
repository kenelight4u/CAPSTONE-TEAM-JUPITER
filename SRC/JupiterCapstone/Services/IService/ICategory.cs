using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    interface ICategory
    {
        IEnumerable<Category> GetAllCategories();//for a user
        void AddCategory(List<Category> category);
        void UpdateCategory(List<Category> category);
        void DeleteCategory(List<Category> category);
        bool SaveChanges();
    }
}
