using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndPoint = "api/Products/";
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productsVM;
        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory= clientFactory;
            _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
        {
			var client = _clientFactory.CreateClient("ProductAPI");

			PutTokenInHeaderAuthorization(token, client);

			using (var response = await client.GetAsync(apiEndPoint))
			{
				if (response.IsSuccessStatusCode)
				{
					var apiResponse = await response.Content.ReadAsStreamAsync();
					productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
				}
				else
				{
					return null;
				}
			}
			return productsVM;
		}

		public async Task<ProductViewModel> FindProdById(int id, string token)
        {
			var client = _clientFactory.CreateClient("ProductAPI");

			PutTokenInHeaderAuthorization(token, client);

			using (var response = await client.GetAsync(apiEndPoint + id))
			{
				if (response.IsSuccessStatusCode)
				{
					var apiResponse = await response.Content.ReadAsStreamAsync();
					productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
				}
				else
				{
					return null;
				}
			}
			return productVM;
		}

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token)
        {
			var client = _clientFactory.CreateClient("ProductAPI");

			PutTokenInHeaderAuthorization(token, client);

			StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");
			using (var response = await client.PostAsync(apiEndPoint, content))
			{
				if (response.IsSuccessStatusCode)
				{
					var apiResponse = await response.Content.ReadAsStreamAsync();
					productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
				}
				else
				{
					return null;
				}
			}
			return productVM;
		}

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token)
        {
			var client = _clientFactory.CreateClient("ProductAPI");

			PutTokenInHeaderAuthorization(token, client);

			ProductViewModel productUpdated = new();
			using (var response = await client.PutAsJsonAsync(apiEndPoint, productVM))
			{
				if (response.IsSuccessStatusCode)
				{
					var apiResponse = await response.Content.ReadAsStreamAsync();
					productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
				}
				else
				{
					return null;
				}
			}
			return productUpdated;
		}

        public async Task<bool> DeleteProdById(int id, string token)
        {
			var client = _clientFactory.CreateClient("ProductAPI");

			PutTokenInHeaderAuthorization(token, client);

			using (var response = await client.DeleteAsync(apiEndPoint + id))
			{
				if (response.IsSuccessStatusCode)
					return true;
			}
			return false;
		}

		private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
		{
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
