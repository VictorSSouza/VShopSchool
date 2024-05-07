using AutoMapper;
using VShopSchool.ProductAPI.DTOs;
using VShopSchool.ProductAPI.Interfaces;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _prodRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository prodRepository, IMapper mapper)
        {
            _prodRepository = prodRepository;
            _mapper = mapper;
        }

        // Get
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var prodsEntity = await _prodRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(prodsEntity);
        }

        public async Task<ProductDTO> GetProdById(int id)
        {
            var prodEntity = await _prodRepository.GetById(id);
            return _mapper.Map<ProductDTO>(prodEntity);
        }

        // Post
        public async Task AddProd(ProductDTO prodDTO)
        {
            var prodEntity = _mapper.Map<Product>(prodDTO);
            await _prodRepository.Create(prodEntity);
            prodDTO.Id = prodEntity.Id;
        }

        // Delete
        public async Task RemoveProd(int id)
        {
            var prodEntity = _prodRepository.GetById(id).Result;
            await _prodRepository.Delete(prodEntity.Id);
        }
        
        // Put
        public async Task UpdateProd(ProductDTO prodDTO)
        {
            var prodEntity = _mapper.Map<Product>(prodDTO);
            await _prodRepository.Update(prodEntity);
        }
    }
}
