using AutoMapper;
using Entities;
using DTOs;

namespace pettsStore
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();

            CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.CategoryName,
                    opts => opts.MapFrom(src => src.Ctegory.CategoryName));
            CreateMap<Category, CategoryDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Products));
        }
    }
}
