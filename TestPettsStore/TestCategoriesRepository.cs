using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class TestCategoriesRepository
    {
        [Fact]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            var category1 = new Category { Id=1, CategoryName="Food" };
            var category2 = new Category { Id=2, CategoryName="Games" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var categories = new List<Category>() { category1, category2 };
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);
            var categoryRepository = new CategoryRepository(mockContext.Object);

            var result = await categoryRepository.getAllCategories();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategoryById_ReturnsCategory()
        {
            var category1 = new Category { Id = 1, CategoryName = "Food" };
            var category2 = new Category { Id = 2, CategoryName = "Games" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var categories = new List<Category>() { category1, category2 };
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);
            var categoryRepository = new CategoryRepository(mockContext.Object);

            var result = await categoryRepository.getCategoryById(category2.Id);

            Assert.Equal(category2, result);
        }
    }
}
