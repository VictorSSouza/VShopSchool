using VShopSchool.CartAPI.Models;

namespace VShopSchool.CartAPI.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProductId { get; set; }
        public int CartHeaderId { get; set; }
        public ProductDTO Product { get; set; } = new ProductDTO();
    }
}
