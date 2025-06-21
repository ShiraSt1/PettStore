using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestProductsRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        private readonly ProductRepository _productRepository;
        public IntegrationTestProductsRepository(DatabaseFixture databaseFixture)
        {
            _pettsStore_DataBaseContext = databaseFixture.Context;
            _productRepository = new ProductRepository(_pettsStore_DataBaseContext);
        }

        [Fact]
        public async Task GetProductById_ValidId_ReturnsProduct()
        {
            var product = new Product { ProductName = "TestProduct", Price = 100, ProductDescription = "Test Description", CtegoryId = 1 };

            await _pettsStore_DataBaseContext.Products.AddAsync(product);
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var result = await _productRepository.getProductById(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.ProductName, result.ProductName);
        }

        [Fact]
        public async Task GetAllProducts_ValidParameters_ReturnsFilteredProducts()
        {
            await _pettsStore_DataBaseContext.Products.AddRangeAsync(new List<Product>
            {
                new Product { ProductName = "Product1", Price = 50, ProductDescription = "Cheap product", CtegoryId = 1 },
                new Product { ProductName = "Product2", Price = 150, ProductDescription = "Expensive product", CtegoryId = 2 },
                new Product { ProductName = "Product3", Price = 75, ProductDescription = "Affordable product", CtegoryId = 1 }
            });

            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var result = await _productRepository.getAllProducts("product", 60, 100, new int?[] { 1 });

            Assert.NotNull(result);
            Assert.Single(result); // Expecting one matching product
            Assert.Equal("Product3", result[0].ProductName);
        }

        //[Fact]
        //public async Task GetAllProducts_NoFilters_ReturnsAllProducts()
        //{
        //    await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
        //    {
        //        new Category { CategoryName = "Category1" },
        //        new Category { CategoryName = "Category2" }
        //    });
        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    await _pettsStore_DataBaseContext.Products.AddRangeAsync(new List<Product>
        //    {
        //        new Product { ProductName = "Product1", Price = 50, ProductDescription = "Description1", CtegoryId = 1 },
        //        new Product { ProductName = "Product2", Price = 150, ProductDescription = "Description2", CtegoryId = 2 }
        //    });

        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    var result = await _productRepository.getAllProducts(null, null, null, null);

        //    Assert.NotNull(result);
        //    Assert.Equal(2, result.Count); // Expecting two products
        //}

        //[Fact]
        //public async Task GetAllProducts_WithPriceFilter_ReturnsFilteredProducts()
        //{
        //    await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
        //    {
        //        new Category { CategoryName = "Category1" },
        //        new Category { CategoryName = "Category2" }
        //    });
        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    await _pettsStore_DataBaseContext.Products.AddRangeAsync(new List<Product>
        //    {
        //        new Product {  ProductName = "Cheap Product", Price = 20, ProductDescription = "Description", CtegoryId = 1 },
        //        new Product {  ProductName = "Medium Product", Price = 50, ProductDescription = "Description", CtegoryId = 1 },
        //        new Product { ProductName = "Expensive Product", Price = 100, ProductDescription = "Description", CtegoryId = 1 }
        //    });

        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    var result = await _productRepository.getAllProducts(null, 30, 70, null);

        //    Assert.NotNull(result);
        //    Assert.Single(result); // Expecting one product within the price range
        //    Assert.Equal("Medium Product", result[0].ProductName);
        //}

        //[Fact]
        //public async Task GetAllProducts_WithCategoryIds_ReturnsFilteredProducts()
        //{
        //    await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
        //    {
        //        new Category { CategoryName = "Category1" },
        //        new Category { CategoryName = "Category2" }
        //    });
        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    await _pettsStore_DataBaseContext.Products.AddRangeAsync(new List<Product>
        //    {
        //        new Product {  ProductName = "Product A", Price = 20, ProductDescription = "Description A", CtegoryId = 1 },
        //        new Product { ProductName = "Product B", Price = 30, ProductDescription = "Description B", CtegoryId = 2 },
        //        new Product { ProductName = "Product C", Price = 40, ProductDescription = "Description C", CtegoryId = 1 }
        //    });

        //    await _pettsStore_DataBaseContext.SaveChangesAsync();

        //    var result = await _productRepository.getAllProducts(null, null, null, new int?[] { 1 });

        //    Assert.NotNull(result);
        //    Assert.Equal(2, result.Count); // Expecting two products in category 1
        //}

        [Fact]
        public async Task GetProductById_ExistingProduct_ReturnsProduct()
        {
            await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
     {
         new Category { CategoryName = "Category1" },
         new Category { CategoryName = "Category2" }
     });
            await _pettsStore_DataBaseContext.SaveChangesAsync();
            var product = new Product { ProductName = "Test Product", Price = 10.0, ProductDescription = "Test Description", CtegoryId = 1 };
            await _pettsStore_DataBaseContext.Products.AddAsync(product);
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var result = await _productRepository.getProductById(1);

            Assert.NotNull(result);
            Assert.Equal(product.ProductName, result.ProductName);
        }

     //   [Fact]
     //   public async Task GetAllProducts_WithFilters_ReturnsFilteredProducts()
     //   {
     //       await _pettsStore_DataBaseContext.Categories.AddRangeAsync(new List<Category>
     //{
     //    new Category { CategoryName = "Category1" },
     //    new Category { CategoryName = "Category2" }
     //});
     //       await _pettsStore_DataBaseContext.SaveChangesAsync();

     //       var product1 = new Product { ProductName = "Test Product 1", Price = 10.0, ProductDescription = "A product", CtegoryId = 1 };
     //       var product2 = new Product { ProductName = "Test Product 2", Price = 20.0, ProductDescription = "Another product", CtegoryId = 2 };
     //       var product3 = new Product { ProductName = "Test Product 3", Price = 30.0, ProductDescription = "A product in category 1", CtegoryId = 1 };

     //       await _pettsStore_DataBaseContext.Products.AddRangeAsync(product1, product2, product3);
     //       await _pettsStore_DataBaseContext.SaveChangesAsync();

     //       var result = await _productRepository.getAllProducts("product", 15, null, new int?[] { 1 });

     //       Assert.NotNull(result);
     //       Assert.Single(result);
     //       Assert.Equal(product3.ProductName, result[0].ProductName);
     //   }

        //[Fact]
        //public async Task GetAllProducts_NoProducts_ReturnsEmptyList()
        //{
        //    var result = await _productRepository.getAllProducts(null, null, null, new int?[] { });

        //    Assert.NotNull(result);
        //    Assert.Empty(result);
        //}
    }
}

