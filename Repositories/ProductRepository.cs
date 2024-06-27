using Microsoft.EntityFrameworkCore;
using TeaStoreApi.Data;
using TeaStoreApi.Interfaces;
using TeaStoreApi.Models;

namespace TeaStoreApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApiDbContext _context;
        public ProductRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await product.Image.CopyToAsync(filestream);
                }
                product.ImageUrl = filePath.Substring(8);


                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if (existingProduct == null)
                {
                    return false;
                }
                _context.Products.Remove(existingProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false;}
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<object>> GetProducts(string productType, int? categoryId = null)
        {
            //return await _context.Products.ToListAsync();
            var productsQuery = _context.Products.AsQueryable();
            if(productType == "category" && categoryId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }
            else if(productType == "bestselling")
            {
                productsQuery = productsQuery.Where(p => p.isBestSelling == true);
            }
            else if(productType == "trending")
            {
                productsQuery = productsQuery.Where(p => p.isTrending == true);
            }
            else
            {
                throw new Exception("Invalid product type");
            }

            var products = await productsQuery.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Imageurl = p.ImageUrl,
            }).ToListAsync();
            return products;
        }

        public async Task<bool> UpdateProduct(int Id, Product product)
        {
            try
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await product.Image.CopyToAsync(filestream);
                }

                var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if (existingProduct == null)
                {
                    return false;
                }
                existingProduct.Name = product.Name;
                existingProduct.Detail = product.Detail;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.isTrending = product.isTrending;
                existingProduct.isBestSelling = product.isBestSelling;
                existingProduct.ImageUrl = filePath.Substring(8);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
