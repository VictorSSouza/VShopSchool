using VShopSchool.Web.Models;

namespace VShopSchool.Web.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> GetCartByUserIdAsync(string userId, string token);
        Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM, string token);
        Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token);
        Task<bool> DeleteCartItemAsync(int cartId, string token);

        // Tarefas em aberto
        Task<bool> ClearCartAsync(string userId, string token);
        Task<bool> ApplyCouponAsync(CartViewModel cartVM, string couponCode, string token);
        Task<bool> RemoveCouponAsync(string userId, string token);

        Task<CartViewModel> CheckoutAsync(CartViewModel cartVM, string token);
    }
}
