using VShopSchool.Web.Models;

namespace VShopSchool.Web.Services.Interfaces
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token);
    }
}
