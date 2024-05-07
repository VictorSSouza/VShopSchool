using VShopSchool.ProductAPI.DTOs;

namespace VShopSchool.ProductAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProdById(int id);
        Task AddProd(ProductDTO prodDTO);
        Task UpdateProd(ProductDTO prodDTO);
        Task RemoveProd(int id);
    }
}
