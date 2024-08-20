using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShopSchool.CouponAPI.Context;
using VShopSchool.CouponAPI.DTOs;
using VShopSchool.CouponAPI.Interfaces;

namespace VShopSchool.CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}
