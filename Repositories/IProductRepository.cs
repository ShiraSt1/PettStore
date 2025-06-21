using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> getAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task<Product> getProductById(int id);
    }
}