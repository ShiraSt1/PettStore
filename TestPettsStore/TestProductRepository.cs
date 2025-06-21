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
    public class TestProductRepository
    {
        [Fact]
        public async Task GetAllProducts_ReturnsAllProducts()
        {
            var product1 = new Product { Id=1,CtegoryId=2, ProductName="CatFood", ProductDescription="yumm", Price=35};
            var product2 = new Product { Id = 2, CtegoryId = 2, ProductName = "DogFood", ProductDescription = "deilshes", Price = 40 };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var products = new List<Product>() { product1, product2 };
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var ProductRepository = new ProductRepository(mockContext.Object);

            var result = await ProductRepository.getAllProducts(null,null,null,[]);

            Assert.Equal(2, result.Count());
            Assert.Equal(product1, products.First());
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            var product1 = new Product { Id = 1, CtegoryId = 2, ProductName = "CatFood", ProductDescription = "yumm", Price = 35 };
            var product2 = new Product { Id = 2, CtegoryId = 2, ProductName = "DogFood", ProductDescription = "deilshes", Price = 40 };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var products = new List<Product>() { product1, product2 };
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var ProductRepository = new ProductRepository(mockContext.Object);

            var result = await ProductRepository.getProductById(product2.Id);

            Assert.Equal(product2, result);
        }
    }
}
