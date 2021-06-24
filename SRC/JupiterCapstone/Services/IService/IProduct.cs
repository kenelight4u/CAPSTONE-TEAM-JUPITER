using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    interface IProduct
    {
        IEnumerable<Product> GetAllProducts();//for a user
        void AddProduct(List<Product> product);
        void UpdateProduct(List<Product> product);
        void DeleteProduct(List<Product> product);
        bool SaveChanges();
    }
}
