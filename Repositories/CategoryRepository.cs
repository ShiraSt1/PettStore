using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        public CategoryRepository(PettsStore_DataBaseContext pettsStore_DataBaseContext)
        {
            _pettsStore_DataBaseContext = pettsStore_DataBaseContext;
        }
        public async Task<Category> getCategoryById(int id)
        {
            Category category = await _pettsStore_DataBaseContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            return category;
        }
        public async Task<List<Category>> getAllCategories()
        {
            return await _pettsStore_DataBaseContext.Categories.ToListAsync();
        }
    }
}
