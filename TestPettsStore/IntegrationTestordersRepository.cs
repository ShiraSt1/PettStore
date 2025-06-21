using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestOrdersRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        private readonly OrderRepository _orderRepository;
        public IntegrationTestOrdersRepository(DatabaseFixture databaseFixture)
        {
            _pettsStore_DataBaseContext = databaseFixture.Context;
            _orderRepository = new OrderRepository(_pettsStore_DataBaseContext);
        }

        [Fact]
        public async Task AddOrder_ValidOrder_ReturnsAddedOrder()
        {
            var category = new Category { CategoryName = "TestCategory" };
            await _pettsStore_DataBaseContext.Categories.AddAsync(category);
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var product = new Product { ProductName = "TestProduct", Price = 100, ProductDescription = "Test Description", CtegoryId = 1 };
            await _pettsStore_DataBaseContext.Products.AddAsync(product);
            await _pettsStore_DataBaseContext.SaveChangesAsync();

            var order = new Order { OrderDate = DateTime.Now, OrderSum = 100, OrderItems = new List<OrderItem> { new OrderItem { ProductId=1, OrderId=1, Quantity=5 }, new OrderItem { ProductId = 1, OrderId = 1, Quantity = 5 } } };
            var result = await _orderRepository.addOrder(order);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.OrderSum, result.OrderSum);
        }

        [Fact]
        public async Task AddOrder_NullOrder_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _orderRepository.addOrder(null));
        }
    }
}