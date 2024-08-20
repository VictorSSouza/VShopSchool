using AutoMapper;
using VShopSchool.CouponAPI.Models;

namespace VShopSchool.CouponAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
        }
    }
}
