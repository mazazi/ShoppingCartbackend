using AutoMapper;
using Tatweer.Application.Responses;
using Tatweer.Application.Responses.Cart;
using Tatweer.Application.Responses.Products;
using Tatweer.Core.Entities;

namespace Tatweer.Application.Mappers;

public class ShoppingCartMappingProfile : Profile
{
    public ShoppingCartMappingProfile()
    {
        CreateMap<CartItem, ShoppingCartDto>()
                                             .ForMember(a => a.ProductName, c => c.MapFrom(opt => opt.Product.Name))
                                             .ReverseMap();
    }
}