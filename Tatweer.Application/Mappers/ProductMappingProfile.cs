using AutoMapper;
using Tatweer.Application.Responses;
using Tatweer.Application.Responses.Products;
using Tatweer.Core.Entities;

namespace Tatweer.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    { 
        CreateMap<Product, ProductsDto>().ReverseMap();
    }
}