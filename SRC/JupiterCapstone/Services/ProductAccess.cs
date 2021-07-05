using JupiterCapstone.Data;
using JupiterCapstone.DTO.Admin;
using JupiterCapstone.DTO.UserDTO;
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
            var products = await _context.Products.Where(e=>e.Quantity !=0).OrderBy(e=>e.CreatedDateTime).ToListAsync();

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
                    ImageUrl=product.ImageUrl,
                    SubCategoryId=product.SubCategoryId

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
            var productsDb = await _context.Products.Where(e=>e.Quantity!=0).OrderBy(e => e.CreatedDateTime).ToListAsync();
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
            var products = await _context.Products.Where(e => e.SubCategoryId == subCategoryId && e.Quantity != 0)
                .OrderBy(p=>p.CreatedDateTime).ToListAsync();
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

        public bool CheckQuantityOfProducts(string productId)
        {
            var productDb = _context.Products.Where(e=>e.Id==productId).FirstOrDefault();
            if (productDb.Quantity!=0)
            {
                productDb.Status = "In Stock";
                _context.SaveChanges();
                return true;
            }
            else 
            {
                productDb.Status = "Out of Stock";
                _context.SaveChanges();
                return false;
            }

           
        }//if youre adding from the available quantity to the cart, then db quantity should reduce a
        public bool ReduceFromProductQuantity(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();
            var availableQuantity =productDb.Quantity-1;
          
            if (availableQuantity>=0)
            {
                productDb.Quantity = availableQuantity;
                productDb.Status = "In Stock";
                _context.SaveChanges();
                return true;
            }
            else 
            {
                productDb.Quantity = 0;
                productDb.Status = "out of Stock";
                _context.SaveChanges();
                return false; 
            }
           
        }
        //if youre removing from the available quantity in the cart, then db quantity should increase
        public bool AddItemToProductQuantity(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();
            var availableQuantity = productDb.Quantity + 1;
            if (availableQuantity > 0)
            {
                productDb.Quantity = availableQuantity;
                productDb.Status = "In Stock";
                _context.SaveChanges();
                return true;
            }
            else 
            {
                productDb.Quantity = availableQuantity;
                productDb.Status = "out of Stock";
                _context.SaveChanges();
                return false;
            }

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
