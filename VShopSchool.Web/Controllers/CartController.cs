using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, ICouponService couponService)
        {
            _cartService = cartService;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            CartViewModel? cartVM = await GetCartByUser();
            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel cartVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.CheckoutAsync(cartVM.CartHeader, await GetAccessToken());

                if (result is not null)
                    return RedirectToAction(nameof(CheckoutCompleted));
            }
            return View(cartVM);
        }
        [HttpGet]
        public IActionResult CheckoutCompleted()
        {
            return View();
        }


        [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartVM)
        {

            if (ModelState.IsValid)
            {
                var result = await _cartService.ApplyCouponAsync(cartVM, await GetAccessToken());
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon()
        {
            var result = await _cartService.RemoveCouponAsync(GetUserId(), await GetAccessToken());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
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
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetDiscountCoupon(cart.CartHeader.CouponCode, await GetAccessToken());

                    if (coupon?.CouponCode is not null)
                    {
                        cart.CartHeader.Discount = coupon.Discount;
                    }
                }

                foreach (var item in cart.CartItems)
                {
                    cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
                }

                cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount -
                                              (cart.CartHeader.TotalAmount
                                              * cart.CartHeader.Discount) / 100;
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
