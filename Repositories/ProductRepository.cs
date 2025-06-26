using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        public ProductRepository(PettsStore_DataBaseContext pettsStore_DataBaseContext)
        {
            _pettsStore_DataBaseContext = pettsStore_DataBaseContext;
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _pettsStore_DataBaseContext.Products.FirstOrDefaultAsync(product => product.Id == id);
           
        }
        public async Task<List<Product>> GetAllProducts(string? desc,int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _pettsStore_DataBaseContext.Products.Where(product =>
            (desc == null ? (true) : (product.ProductDescription.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CtegoryId))))
                .OrderBy(product => product.Price);

            List<Product> products = await query.ToListAsync();
            return products;
        }
    }
}