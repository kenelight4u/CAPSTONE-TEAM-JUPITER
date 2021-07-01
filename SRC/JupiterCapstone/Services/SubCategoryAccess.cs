using JupiterCapstone.Data;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.DTO.Admin;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class SubCategoryAccess : ISubCategory
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryAccess(ApplicationDbContext context)
        {
            _context = context;

        }
     
        public async Task<bool> AddSubCategoryAsync(List<AddSubCategoryDto> subCategories)
        {
            if (subCategories.Count == 0)
            {
                return false;
                
            }
            else
            {
                foreach (var subCategoriesToAdd in subCategories)
                {
                    var checkcategory = await _context.SubCategories.FirstOrDefaultAsync(e => e.SubCategoryName == subCategoriesToAdd.SubCategoryName);
                    if (checkcategory != null)
                    {
                        return false;
                    }

                    SubCategory subCategoryDb = new SubCategory()
                    {
                        Id = Guid.NewGuid().ToString(),
                        SubCategoryName = subCategoriesToAdd.SubCategoryName,
                        CategoryId=subCategoriesToAdd.CategoryId
                    };
                    await _context.SubCategories.AddAsync(subCategoryDb);

                }
                await SaveChangesAsync();
                return true;
            }
        }

        public async Task DeleteSubCategoryAsync(List<string> deleteSubcategories)
        {
            var allSubCategories = await _context.SubCategories.Include(e => e.Products).ToListAsync();

            foreach (var subcategory in deleteSubcategories)
            {
                var dbSubCategory = allSubCategories.FirstOrDefault(e => e.Id == subcategory);
                _context.SubCategories.Remove(dbSubCategory);

            }
            await SaveChangesAsync();
        }

       /* public async Task<IEnumerable<ViewSubCategoryDto>> GetAllSubCategoriesAsync()
        {
            var allSubCategories = await _context.SubCategories.ToListAsync();
            List<ViewSubCategoryDto> viewsubCategory = new List<ViewSubCategoryDto>();

            foreach (var subCategory in allSubCategories)
            {
                viewsubCategory.Add(new ViewSubCategoryDto()
                {
                    SubCategoryId = subCategory.Id,
                    SubCategoryName = subCategory.SubCategoryName,   

                });
            }
            return viewsubCategory;
        }*/


        public async Task<bool> UpdateSubCategoryAsync(List<UpdateSubCategoryDto> updateSubcategories)
        {
            if (updateSubcategories.Count==0)
            {
                return false;
            }
            else
            {
                var oldCategory = await _context.SubCategories.ToListAsync();
                foreach (var subcategory in updateSubcategories)
                {
                    var categoryDb = oldCategory.FirstOrDefault(e => e.Id == subcategory.SubCategoryId);
                    categoryDb.SubCategoryName = subcategory.SubCategoryName;

                }
                await SaveChangesAsync();
                return true;
            }
         
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewSubCategoryDto>> GetSubCategoriesByCategoryIdAsync(string categoryId)
        {
            var subCategories = await _context.SubCategories.Where(e=>e.CategoryId==categoryId).ToListAsync();
            if (subCategories.Count==0)
            {
                return null;
            }
            else
            {
                List<ViewSubCategoryDto> foundCategories = new List<ViewSubCategoryDto>();
                foreach (var subcategories in subCategories)
                {
                    foundCategories.Add(new ViewSubCategoryDto
                    {
                        SubCategoryId = subcategories.Id,
                        SubCategoryName = subcategories.SubCategoryName

                    });

                }
                return foundCategories;

            }
           
        }



        
    }

}
