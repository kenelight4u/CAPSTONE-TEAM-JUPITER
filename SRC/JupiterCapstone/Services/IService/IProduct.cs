using JupiterCapstone.DTO.Admin;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IProduct
    {
        Task<IEnumerable<ViewProductDto>> GetAllProductsAsync();//for a user
        Task<bool> AddProductAsync(List<AddProductDto> productsDto); 
        Task<bool> UpdateProductAsync(List<UpdateProductDto> updateProductsDto);  
        Task DeleteProductAsync(List<string>productToDelete);
        Task<IEnumerable<ViewProductDto>> GetProductsByNameAsync(List<string>products);
        Task<IEnumerable<ViewProductDto>> GetProductsBySubCategoryIdAsync(string subCategoryId);

        bool CheckQuantityOfProducts(string productId);
        bool ReduceFromProductQuantity(string productId);
        bool AddItemToProductQuantity(String productId);



    }
}
