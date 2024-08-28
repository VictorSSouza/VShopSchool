using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndPoint = "/api/cart";
        private CartViewModel cartVM = new();
        private CartHeaderViewModel cartHdVM = new();

        public CartService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CartViewModel> GetCartByUserIdAsync(string userId, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync($"{apiEndPoint}/getcart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cartVM;
        }

        public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            StringContent content = new(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/addcart/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cartVM;
        }

        public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            CartViewModel cartUpdated = new();

            using (var response = await client.PutAsJsonAsync($"{apiEndPoint}/updatecart/", cartVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartUpdated = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cartUpdated;
        }

        public async Task<bool> DeleteCartItemAsync(int cartId, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndPoint}/deletecart/" + cartId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ClearCartAsync(string userId, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndPoint}/cleancart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<bool> ApplyCouponAsync(CartViewModel cartVM, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            StringContent content = new(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/applycoupon/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveCouponAsync(string userId, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndPoint}/deletecoupon/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<CartHeaderViewModel> CheckoutAsync(CartHeaderViewModel cartHeaderVM, string token)
        {
            var client = _clientFactory.CreateClient("CartApi");
            PutTokenInHeaderAuthorization(token, client);

            StringContent content = new(JsonSerializer.Serialize(cartHeaderVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/checkout/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartHdVM = await JsonSerializer.DeserializeAsync<CartHeaderViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cartHdVM;
        }
    }
}
