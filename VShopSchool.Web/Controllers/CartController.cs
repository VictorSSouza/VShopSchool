using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            CartViewModel? cartVM = await GetCartByUser();

            if (cartVM is null)
            {
                ModelState.AddModelError("CartNotFound", "Este carrinho não existe, volte para a loja...");
                return View("/Views/Cart/CartNotFound.cshtml");
            }
            return View(cartVM);
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await _cartService.DeleteCartItemAsync(id, await GetAccessToken());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(id);
        }

        private async Task<CartViewModel?> GetCartByUser()
        {
            var cart = await _cartService.GetCartByUserIdAsync(GetUserId(), await GetAccessToken());

            if (cart?.CartHeader is not null)
            {
                foreach (var item in cart.CartItems)
                {
                    cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
                }
            }
            return cart;

        }

        private string GetUserId()
        {
            return User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}
