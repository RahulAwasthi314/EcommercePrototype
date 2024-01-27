using AutoMapper;
using EcommercePrototype.Core.Dto;
using EcommercePrototype.Core.Entity;

namespace EcommercePrototype.Business.Utitity
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductDto>().ReverseMap();
        }

    }
}
