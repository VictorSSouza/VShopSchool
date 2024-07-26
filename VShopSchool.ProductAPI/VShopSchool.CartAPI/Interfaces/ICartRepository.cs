using VShopSchool.CartAPI.DTOs;

namespace VShopSchool.CartAPI.Interfaces
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartByUserIdAsync(string userId);
        Task<CartDTO> UpdateCartAsync(CartDTO cartDTO);
        Task<bool> DeleteItemCartAsync(int cartItemId);
        Task<bool> CleanCartAsync(string userId);

        Task<bool> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> DeleteCouponAsync(string userId);

    }
}
