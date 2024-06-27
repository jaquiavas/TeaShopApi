using TeaStoreApi.Models;

namespace TeaStoreApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<object>> GetProducts(string productType, int? categoryId = null);
        Task<Product> GetProductById(int productId);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(int Id, Product product);
        Task<bool> DeleteProduct(int Id);
    }
}
