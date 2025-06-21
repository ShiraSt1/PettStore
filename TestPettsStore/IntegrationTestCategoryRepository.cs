using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestCategoryRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        private readonly CategoryRepository _categoryRepository;
        public IntegrationTestCategoryRepository(DatabaseFixture databaseFixture)
        {
            _pettsStore_DataBaseContext = databaseFixture.Context;
            _categoryRepository = new CategoryRepository(_pettsStore_DataBaseContext);
        }

        [Fact]
        public async Task GetAllCategories_NoCategories_ReturnsEmptyList()
        {
            var result = await _categoryRepository.getAllCategories();

            Assert.NotNull(result);
            Assert.Empty(result); // Expecting an empty list
        }

        [Fact]
        public async Task GetCategoryById_ValidId_ReturnsCategory()
        {
            var category = new Category {  CategoryName = "TestCategory" };
            await _pettsStore_DataBaseContext.Categories.AddAsync(category);
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var result = await _categoryRepository.getCategoryById(category.Id);

            Assert.NotNull(result);
            Assert.Equal(category.CategoryName, result.CategoryName);
        }

        [Fact]
        public async Task GetCategoryById_NonExistingId_ReturnsNull()
        {
            var result = await _categoryRepository.getCategoryById(999); // Assuming this ID does not exist

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
            {
                new Category { CategoryName = "Category1" },
                new Category { CategoryName = "Category2" }
            });
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var result = await _categoryRepository.getAllCategories();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Expecting two categories
        }
    }
}