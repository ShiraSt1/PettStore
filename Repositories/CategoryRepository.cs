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
        public async Task<Category> GetCategoryById(int id)
        {
            return await _pettsStore_DataBaseContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
        }
        public async Task<List<Category>> getAllCategories()//GetAllCategories
        {
            return await _pettsStore_DataBaseContext.Categories.ToListAsync();
        }
    }
}
