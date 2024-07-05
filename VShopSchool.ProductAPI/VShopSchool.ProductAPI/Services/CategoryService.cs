using AutoMapper;
using System.Collections.Generic;
using VShopSchool.ProductAPI.DTOs;
using VShopSchool.ProductAPI.Interfaces;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository;
            _mapper = mapper;
        }

        // Get
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var catsEntity = await _catRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(catsEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var catsEntity = await _catRepository.GetCategoriesProducts();
            return _mapper.Map<IEnumerable<CategoryDTO>>(catsEntity);
        }

        public async Task<CategoryDTO> GetCatById(int id)
        {
            var catEntity = await _catRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(catEntity);
        }

        // Post
        public async Task AddCat(CategoryDTO catDTO)
        {
            var catEntity = _mapper.Map<Category>(catDTO);
            await _catRepository.Create(catEntity);
            catDTO.CategoryId = catEntity.CategoryId;
        }

        // Put
        public async Task UpdateCat(CategoryDTO catDTO)
        {
            var catEntity = _mapper.Map<Category>(catDTO);
            await _catRepository.Update(catEntity);
        }

        // Delete
        public async Task RemoveCat(int id)
        {
            var catEntity = _catRepository.GetById(id).Result;
            await _catRepository.Delete(catEntity.CategoryId);
        }
    }
}
