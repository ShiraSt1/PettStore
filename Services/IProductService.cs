using DTOs;

namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> getAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task<ProductDTO> getProductById(int id);
    }
}