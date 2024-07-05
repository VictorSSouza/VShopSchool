using VShopSchool.Web.Models;

namespace VShopSchool.Web.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token);
	}
}
