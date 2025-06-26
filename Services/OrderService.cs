using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Entities;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;//_orderRepository;
        IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;//_orderRepository = orderRepository
            _mapper = mapper;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order) // Change to PascalCase: AddOrder
        {
            return _mapper.Map<Order, OrderDTO>(await orderRepository.addOrder(_mapper.Map<OrderDTO,Order>(order)));
        }
    }
}
