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
        Task<ViewProductDto> GetProductByIdAsync(string productId);
        bool CheckQuantityOfProducts(string productId);
        void DecreaseProductQuantity(string productId);
        void IncreaseProductQuantity(string productId);

        string InStoreStatus(string productId);
        void IncreaseProductQuantityByNumberOfQuantity(string productId, double quantity);


    }
}
