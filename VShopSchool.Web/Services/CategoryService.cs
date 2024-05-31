using System.Text.Json;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IHttpClientFactory _clientFactory;
		private const string apiEndPoint = "api/Categories/";
		private readonly JsonSerializerOptions _options;

		public CategoryService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
			_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}
		public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
		{
			var client = _clientFactory.CreateClient("ProductAPI");
			IEnumerable<CategoryViewModel> categories;

			var response = await client.GetAsync(apiEndPoint);

			if (response.IsSuccessStatusCode)
			{
				var apiResponse = await response.Content.ReadAsStreamAsync();
				categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
			}
			else
			{
				return null;
			}
			return categories;

		}
	}
}
