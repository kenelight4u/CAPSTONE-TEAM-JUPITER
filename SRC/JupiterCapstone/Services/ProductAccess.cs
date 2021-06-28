using JupiterCapstone.Data;
using JupiterCapstone.Dtos.Admin;
using JupiterCapstone.Dtos.User;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace JupiterCapstone.Services
{
    public class ProductAccess : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> AddProductAsync(List<AddProductDto> productsDto)
        {
            if (productsDto.Count==0)
            {
                return false;

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
                        SubCategoryId=product.SubCategoryId,
                        ImageUrl=UploadImage(product.ImageUrl),
                        
                    };
                   await _context.Products.AddAsync(productdb);
                }
               
               await SaveChangesAsync();
                return true;
            }
           
        }

        public async Task DeleteProductAsync(List<string> productToDelete)
        {
            var allProducts = await _context.Products.ToListAsync();
  
                foreach (var product in productToDelete)
                {
                    var dbProduct = allProducts.FirstOrDefault(e => e.Id==product);
                    _context.Products.Remove(dbProduct);
                   

                }
                  await SaveChangesAsync();

        }

        public async Task<IEnumerable<ViewProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
        
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


        public async Task<bool> UpdateProductAsync(List<UpdateProductDto> updateProductsDto) 
        {
            if (updateProductsDto.Count==0)
            {
                return false;
            }
           
            else
            {
                var oldProducts = await _context.Products.ToListAsync();
                foreach (var product in updateProductsDto)
                {
                    var dbProduct = oldProducts.FirstOrDefault(e => e.Id == product.ProductId);
                    dbProduct.Price = product.Price;
                    dbProduct.Description = product.Description;
                    dbProduct.SupplierName = product.SupplierName;
                    dbProduct.ProductName = product.ProductName;
                    dbProduct.Quantity = product.Quantity;
                    //dbProduct.IsDeleted = product.IsDeleted;
                    dbProduct.Status = product.Status;
                    dbProduct.ImageUrl = UploadImage(product.ImageUrl);

                }
                await SaveChangesAsync();
                return true;
            }
           
            
        }
        public async Task<IEnumerable<ViewProductDto>> GetProductsByNameAsync(List<string> products)
        {
            var productsDb = await _context.Products.ToListAsync();
            List<ViewProductDto> viewProduct = new List<ViewProductDto>();
            if (products.Count==0)
            {
                return null;
            }
            else
            {
               
                foreach (var product in products)
                {
                    
                    var allProducts = productsDb.FindAll(e => e.ProductName.ToLower().Replace(" ", "") == product.ToLower().Replace(" ", ""));
                    if (allProducts.Count==0)
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
        public async Task<IEnumerable<ViewProductDto>> GetProductsBySubCategoryIdAsync(string subCategoryId)
        {
            var products = await _context.Products.Where(e => e.SubCategoryId == subCategoryId).ToListAsync();
            if (products.Count == 0)
            {
                return null;

            }
            else
            {
                List<ViewProductDto> productsdto = new List<ViewProductDto>();
                foreach (var product in products)
                {
                    productsdto.Add(new ViewProductDto()
                    {
                        ProductId = product.Id,
                        Price = product.Price,
                        Description = product.Description,
                        SupplierName = product.SupplierName,
                        Quantity = product.Quantity,
                        ProductName = product.ProductName,
                        Status = product.Status,
                        ImageUrl = product.ImageUrl,


                    });

                }
                return productsdto;
            }
        }
          

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
        public string UploadImage(string filePath)
        {
            Account account = new Account()
            {
                ApiKey = "199453743568777",
                ApiSecret = "kWP88_SyXMQjd3MdiW-WotDNpoc",
                Cloud = "paulayomikun",
            };
   
            Cloudinary cloudinary = new Cloudinary(account);
            //cloudinary.Api.Secure = true;
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                Folder = "AduabaProduct",
                Invalidate = true
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
