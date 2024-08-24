using System.Net.Http.Headers;
using System.Text.Json;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndPoint = "api/coupon";
        private CouponViewModel couponVM = new();

        public CouponService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token)
        {
            var client = _clientFactory.CreateClient("CouponApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync($"{apiEndPoint}/{couponCode}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    couponVM = await JsonSerializer.DeserializeAsync<CouponViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return couponVM;
        }
        
        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
