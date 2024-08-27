namespace VShopSchool.CartAPI.DTOs
{
    public class CheckoutHeaderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = 0.00m; // Valor total
        public decimal Discount { get; set; } = 0.00m; // Valor do desconto

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Cartao de credito
        public string CardNumber { get; set; } = string.Empty;
        public string NameOnCard { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string ExpireMonthYear { get; set; } = string.Empty;

        // Informacoes do carrinho durante o checkout
        public int CartTotalItems { get; set; }
        public IEnumerable<CartItemDTO>? CartItems { get; set; }

    }
}
