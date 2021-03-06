using JupiterCapstone.Data;
using JupiterCapstone.DTO.Admin;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JupiterCapstone.Services
{
    public class ProductAccess : IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public ProductAccess(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<bool> AddProductAsync(List<AddProductDto> productsDto)
        {
            if (productsDto.Count == 0)
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
                       // Status = product.Status,
                        SubCategoryId=product.SubCategoryId,
                        ImageUrl= _imageService.UploadImage(product.ImageUrl),
                        
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
                    var dbProduct = allProducts.FirstOrDefault(e => e.Id == product);
                    _context.Products.Remove(dbProduct);
                   
                }

                  await SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.OrderBy(e => e.CreatedDateTime ).ToListAsync();
            if (products.Count==0)
            {
                return null;
            }

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
                    Status = InStoreStatus(product.Id),                
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
                    dbProduct.ImageUrl = _imageService.UploadImage(product.ImageUrl);

                }
                await SaveChangesAsync();
                return true;
            }
           
            
        }
        public async Task<IEnumerable<ViewProductDto>> GetProductsByNameAsync(List<string> products)
        {
            var productsDb = await _context.Products.OrderBy(e => e.CreatedDateTime).ToListAsync();
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
                                Status = InStoreStatus(aProduct.Id),
                                ImageUrl = aProduct.ImageUrl,
                                SubCategoryId=aProduct.SubCategoryId
                               
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
            var products = await _context.Products.Where(e => e.SubCategoryId == subCategoryId)
                .OrderBy(p => p.CreatedDateTime).ToListAsync();
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
                        Status = InStoreStatus(product.Id),
                        ImageUrl = product.ImageUrl,
                        SubCategoryId=product.SubCategoryId

                    });

                }
                return productsdto;
            }
        }
        public async Task<ViewProductDto> GetProductByIdAsync(string productId)
        {
            var product = await _context.Products.Where(e => e.Id == productId).OrderBy(e=>e.CreatedDateTime).FirstOrDefaultAsync();
            if (product==null)
            {
                return null;
            }
            ViewProductDto viewProduct = new ViewProductDto()
            {
                ProductId = product.Id,
                Price = product.Price,
                Description = product.Description,
                SupplierName = product.SupplierName,
                Quantity = product.Quantity,
                ProductName = product.ProductName,
                Status = InStoreStatus(product.Id),
                ImageUrl = product.ImageUrl,
                SubCategoryId=product.SubCategoryId
            };
            return viewProduct;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public string InStoreStatus(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();

            if (productDb.Quantity > 0)
            {
                //I will need this in wishlist section
                productDb.Status = "In Stock";

                return productDb.Status;
                
            }
            else
            {
                productDb.Status = "Out of Stock";

                return productDb.Status;
            }
        }

        public bool CheckQuantityOfProducts(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();

            if (productDb.Quantity > 0)
            {
                return true;
            }
            else 
            {
                return false;
            } 
        }
        
        //if you're adding from the available quantity to the cart, then db quantity should reduce
        public void DecreaseProductQuantity(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();
            productDb.Quantity--;
            _context.SaveChanges();
           
        }
        //if you're removing from the available quantity in the cart, then db quantity should increase
        public void IncreaseProductQuantity(string productId)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();
            productDb.Quantity++;
            _context.SaveChanges();

        }
        public void IncreaseProductQuantityByNumberOfQuantity(string productId, double quantity)
        {
            var productDb = _context.Products.Where(e => e.Id == productId).FirstOrDefault();
            var availableProduct=productDb.Quantity+quantity;
            productDb.Quantity = availableProduct;
            _context.SaveChanges();

        }

        
    }
}
