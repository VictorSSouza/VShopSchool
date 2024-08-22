using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.CouponAPI.DTOs;
using VShopSchool.CouponAPI.Interfaces;

namespace VShopSchool.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCode(couponCode);
            if (coupon is null)
            {
                return NotFound($"Código do cupom: {couponCode} não encontrado");
            }
            return Ok(coupon);
        }
    }
}
