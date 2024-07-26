using System.ComponentModel.DataAnnotations;

namespace VShopSchool.CartAPI.DTOs
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Id do usuário é obrigatório")]
        public string? UserId { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;
    }
}
