using VShopSchool.Web.Models;

namespace VShopSchool.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
        Task<ProductViewModel> FindProdById(int id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token);
        Task<bool> DeleteProdById(int id, string token);
    }
}
