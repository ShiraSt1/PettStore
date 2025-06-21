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
    public class TestOrsdersRepository
    {
        [Fact]
        public async Task CreateOrder_AddOrder()
        {
            var order = new Order { Id = 1, UserId = 1, OrderSum = 100, OrderDate = new DateTime(2025, 1, 1), OrderItems = [new OrderItem { Id = 1, ProductId = 1, OrderId = 1, Quantity = 10 }] };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var orders = new List<Order>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var ordersRepository = new OrderRepository(mockContext.Object);

            var result = await ordersRepository.addOrder(order);

            mockContext.Verify(x => x.Orders.AddAsync(order, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
