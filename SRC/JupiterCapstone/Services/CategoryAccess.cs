using JupiterCapstone.Data;
using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class CategoryAccess : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public void AddCategory(List<AddCategoryDto> categoriesToAdd)
        {
            if (categoriesToAdd==null)
            {
                throw new ArgumentNullException(nameof(categoriesToAdd));

            }
            else
            {
                foreach (var category in categoriesToAdd)
                {
                    Category categoryDb = new Category()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CategoryName = category.CategoryName

                    };
                    _context.Categories.Add(categoryDb);
                    SaveChanges();


                }
            }
        }

        public void DeleteCategory(List<string> categoriesToDelete)
        {
            var allCategories = _context.Categories.ToList();

            foreach (var category in categoriesToDelete)
            {
                var dbCategory = allCategories.FirstOrDefault(e => e.Id == category);
                _context.Categories.Remove(dbCategory);


            }
            SaveChanges();
        }

        public IEnumerable<ViewCategoryDto> GetAllCategories()
        {
            var allCategories = _context.Categories.ToList();
            List<ViewCategoryDto> viewCategories = new List<ViewCategoryDto>();
            foreach (var category in allCategories)
            {
                viewCategories.Add(new ViewCategoryDto() { 
                    CategoryId=category.Id,
                    CategoryName=category.CategoryName
        
                
                });;

            }
            return viewCategories;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateCategory(List<UpdateCategoryDto> categoriesToUpdate)
        {
            var oldCategory = _context.Categories.ToList();
            foreach (var category in categoriesToUpdate)
            {
                var categoryDb = oldCategory.FirstOrDefault(e => e.Id ==category.CategoryId);
                categoryDb.CategoryName = category.CategoryName;

            }
            SaveChanges();
        }
    }
}
