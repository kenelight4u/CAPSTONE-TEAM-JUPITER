using JupiterCapstone.Data;
using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddCategoryAsync(List<AddCategoryDto> categoriesToAdd)
        {
            if (categoriesToAdd.Count==0)
            {
                return false;
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
                   await _context.Categories.AddAsync(categoryDb);
                    await SaveChangesAsync();

                }
                return true;
            }
        }


        public async Task DeleteCategoryAsync(List<string> categoriesToDelete)
        {
            var allCategories = await _context.Categories.Include(e=>e.SubCategories).ToListAsync();

            foreach (var category in categoriesToDelete)
            {
                var dbCategory = allCategories.FirstOrDefault(e => e.Id == category);                  
                  _context.Categories.Remove(dbCategory);
                
            }
            await SaveChangesAsync();


        }

        public async Task<IEnumerable<ViewCategoryDto>> GetAllCategoriesAsync()
        {
            var allCategories = await _context.Categories.ToListAsync();
            List<ViewCategoryDto> viewCategories = new List<ViewCategoryDto>();
            foreach (var category in allCategories)
            {
                viewCategories.Add(new ViewCategoryDto() { 
                    CategoryId=category.Id,
                    CategoryName=category.CategoryName       
                
                });

            }
            return viewCategories;
        }


        public async Task<bool> UpdateCategoryAsync(List<UpdateCategoryDto> categoriesToUpdate)
        {
            if (categoriesToUpdate.Count==0)
            {
                return false;

            }
            else
            {
                var oldCategory = await _context.Categories.ToListAsync();
                foreach (var category in categoriesToUpdate)
                {
                    var categoryDb = oldCategory.FirstOrDefault(e => e.Id == category.CategoryId);
                    categoryDb.CategoryName = category.CategoryName;

                }
                await SaveChangesAsync();
                return true;
            }
            
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
