using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IProduct
    {
        IEnumerable<ViewProductDto> GetAllProducts();//for a user
        void AddProduct(List<AddProductDto> productsDto); 
        void  UpdateProduct(List<UpdateProductDto> updateProductsDto);  
        void DeleteProduct(List<string>productToDelete);
        IEnumerable<ViewProductDto> GetProductsByName(List<string>products);
        void SaveChanges();

    }
}
