namespace VShopSchool.Web.Models
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; } = new CartHeaderViewModel();
        public IEnumerable<CartItemViewModel>? CartItems { get; set; } = Enumerable.Empty<CartItemViewModel>();
    }
}
