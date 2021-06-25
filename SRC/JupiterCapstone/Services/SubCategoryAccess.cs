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
    public class SubCategoryAccess : ISubCategory
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public void AddSubCategory(List<AddSubCategoryDto> addSubcategories)
        {
            if (addSubcategories == null)
            {
                throw new ArgumentNullException(nameof(addSubcategories));

            }
            else
            {
                foreach (var subCategory in addSubcategories)
                {
                    SubCategory subCategoryDb = new SubCategory()
                    {
                        Id = Guid.NewGuid().ToString(),
                        SubCategoryName = subCategory.SubCategoryName,

                    };
                    _context.SubCategories.Add(subCategoryDb);
                    SaveChanges();

                }
            }
        }

        public void DeleteSubCategory(List<string> deleteSubcategories)
        {
            var allsubCategories = _context.SubCategories.ToList();

            foreach (var subcategory in deleteSubcategories)
            {
                var dbSubCategory = allsubCategories.FirstOrDefault(e => e.Id == subcategory);
                _context.SubCategories.Remove(dbSubCategory);


            }
            SaveChanges();
        }

        public IEnumerable<ViewSubCategoryDto> GetAllSubCategories()
        {
            var allSubCategories = _context.SubCategories.ToList();
            List<ViewSubCategoryDto> viewsubCategory = new List<ViewSubCategoryDto>();

            foreach (var subCategory in allSubCategories)
            {
                viewsubCategory.Add(new ViewSubCategoryDto()
                {
                    SubCategoryId = subCategory.Id,
                    SubCategoryName = subCategory.SubCategoryName

                });
            }
            return viewsubCategory;
        }


        public void UpdateSubCategory(List<UpdateSubCategoryDto> updateSubcategories)
        {
            var oldCategory = _context.SubCategories.ToList();
            foreach (var subcategory in updateSubcategories)
            {
                var categoryDb = oldCategory.FirstOrDefault(e => e.Id == subcategory.SubCategoryId);
                categoryDb.SubCategoryName = subcategory.SubCategoryName;

            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
