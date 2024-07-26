using AutoMapper;
using VShopSchool.CartAPI.Models;

namespace VShopSchool.CartAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
