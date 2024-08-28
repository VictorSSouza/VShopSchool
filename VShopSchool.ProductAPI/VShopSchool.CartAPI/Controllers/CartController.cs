using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.CartAPI.DTOs;
using VShopSchool.CartAPI.Interfaces;

namespace VShopSchool.CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getcart/{userId}")]
        public async Task<ActionResult<CartDTO>> GetByUserId(string userId)
        {
            var cartDTO = await _repository.GetCartByUserIdAsync(userId);

            if (cartDTO is null)
                return NotFound();

            return Ok(cartDTO);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutDTO)
        {
            var cart = await _repository.GetCartByUserIdAsync(checkoutDTO.UserId);

            if (cart is null)
                return NotFound($"Carrinho não encontrado para {checkoutDTO.UserId}");

            checkoutDTO.CartItems = cart.CartItems;
            checkoutDTO.DateTime = DateTime.Now;

            return Ok(checkoutDTO);
        }

        [HttpPost("addcart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _repository.UpdateCartAsync(cartDTO);

            if (cart is null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("updatecart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            var cart = await _repository.UpdateCartAsync(cartDTO);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("deletecart/{id}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            var status = await _repository.DeleteItemCartAsync(id);

            if (!status)
                return BadRequest();

            return Ok(status);
        }

        [HttpDelete("cleancart/{userId}")]
        public async Task<ActionResult<bool>> RemoveCartAll(string userId)
        {
            var status = await _repository.CleanCartAsync(userId);

            if(!status)
                return BadRequest();

            return Ok(status);
        }

        [HttpPost("applycoupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            // Usa o id e o código do cupom que pertence ao carrinho para o metodo de aplicar desconto
            var result = await _repository.ApplyCouponAsync(cartDTO.CartHeader.UserId,
                                                            cartDTO.CartHeader.CouponCode);

            if (!result)
                return NotFound($"Header do carrinho não encontrado para usuário com id = {cartDTO.CartHeader.UserId}");

            return Ok(result);
        }

        [HttpDelete("deletecoupon/{userId}")]
        public async Task<ActionResult<CartDTO>> DeleteCoupon(string userId)
        {
            // Usa o id do carrinho para remover o cupom de desconto do valor total
            var result = await _repository.DeleteCouponAsync(userId);

            if (!result)
                return NotFound($"Cupom de desconto não encontrado para o usuário com id = {userId}");

            return Ok(result);
        }
    }
}
