using VShopSchool.ProductAPI.DTOs;

namespace VShopSchool.ProductAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
        Task<CategoryDTO> GetCatById(int id);
        Task AddCat(CategoryDTO catDTO);
        Task UpdateCat(CategoryDTO catDTO);
        Task RemoveCat(int id);
    }
}
