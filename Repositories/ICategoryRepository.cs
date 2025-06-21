using Entities;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> getAllCategories();
        Task<Category> getCategoryById(int id);
    }
}