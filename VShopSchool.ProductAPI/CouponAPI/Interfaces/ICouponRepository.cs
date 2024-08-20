using VShopSchool.CouponAPI.DTOs;

namespace VShopSchool.CouponAPI.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
