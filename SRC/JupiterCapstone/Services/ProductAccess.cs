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
    public class ProductAccess : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public void AddProduct(List<AddProductDto> productsDto)
        {
            if (productsDto==null)
            {
                throw new ArgumentNullException(nameof(productsDto));

            }
            else
            {
               
                foreach (var product in productsDto)
                {
                    Product productdb = new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Price = product.Price,
                        Description = product.Description,
                        ProductName = product.ProductName,
                        Quantity = product.Quantity,
                        SupplierName = product.SupplierName,
                        Status = product.Status,
                        ImageUrl=product.ImageUrl,
                        
                    };
                    _context.Products.Add(productdb);
                }
               
                SaveChanges();
            }
           
        }

        public void DeleteProduct(List<string> productToDelete)
        {
            var allProducts = _context.Products.ToList();
  
                foreach (var product in productToDelete)
                {
                    var dbProduct = allProducts.FirstOrDefault(e => e.Id==product);
                    _context.Products.Remove(dbProduct);
                   

                }
                  SaveChanges();
            

        }

        public IEnumerable<ViewProductDto> GetAllProducts()
        {
            var products = _context.Products.ToList();
        
            List<ViewProductDto> viewProduct = new List<ViewProductDto>();

            foreach (var product in products)
            {
                viewProduct.Add(new ViewProductDto
                {
                    ProductId = product.Id,
                    Price = product.Price,
                    Description = product.Description,
                    SupplierName = product.SupplierName,
                    Quantity = product.Quantity,
                    ProductName = product.ProductName,
                    Status = product.Status,
                    ImageUrl=product.ImageUrl

                }
              );

            }
            return viewProduct;
        }


        public void UpdateProduct(List<UpdateProductDto> updateProductsDto) 
        {
            var oldProducts = _context.Products.ToList();

            foreach (var product in updateProductsDto)
            {
                var dbProduct = oldProducts.FirstOrDefault(e=>e.Id==product.ProductId);
                dbProduct.Price = product.Price;
                dbProduct.Description = product.Description;
                dbProduct.SupplierName = product.SupplierName;
                dbProduct.ProductName = product.ProductName;
                dbProduct.Quantity = product.Quantity;
                //dbProduct.IsDeleted = product.IsDeleted;
                dbProduct.Status = product.Status;
                dbProduct.ImageUrl = product.ImageUrl;


            }
            SaveChanges();
            
        }
        public IEnumerable<ViewProductDto> GetProductsByName(List<string> products)
        {
            var productsDb = _context.Products.ToList();
            List<ViewProductDto> viewProduct = new List<ViewProductDto>();
            if (products.Count==0)
            {
                throw new ArgumentNullException(nameof(products));
            }
            else
            {
               
                foreach (var product in products)
                {
                    
                    var allProducts = productsDb.FindAll(e => e.ProductName.ToLower().Replace(" ", "") == product.ToLower().Replace(" ", ""));
                    if (allProducts==null)
                    {                      
                        return null;

                    }
                    else
                    {
                        foreach (var aProduct in allProducts)
                        {
                            viewProduct.Add(new ViewProductDto
                            {
                                ProductId = aProduct.Id,
                                Price = aProduct.Price,
                                Description = aProduct.Description,
                                SupplierName = aProduct.SupplierName,
                                Quantity = aProduct.Quantity,
                                ProductName = aProduct.ProductName,
                                Status = aProduct.Status,
                                ImageUrl = aProduct.ImageUrl
                                //IsDeleted = aProduct.IsDeleted


                            });

                        }
                        

                    }                   
                  
                }
                return viewProduct;

            }
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
