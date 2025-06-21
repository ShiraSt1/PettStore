using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        public OrderRepository(PettsStore_DataBaseContext pettsStore_DataBaseContext)
        {
            _pettsStore_DataBaseContext = pettsStore_DataBaseContext;
        }
        public async Task<Order> addOrder(Order order)
        {
            await _pettsStore_DataBaseContext.Orders.AddAsync(order);
            await _pettsStore_DataBaseContext.SaveChangesAsync();
            return order;
        }
    }
}
