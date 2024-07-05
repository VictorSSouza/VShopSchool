using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.ProductAPI.DTOs;
using VShopSchool.ProductAPI.Interfaces;
using VShopSchool.ProductAPI.Roles;

namespace VShopSchool.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _catService;
        public CategoriesController(ICategoryService catService)
        {
            _catService = catService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var catsDTO = await _catService.GetCategories();

            if (catsDTO is null)
                return NotFound("Categorias não encontradas");

            return Ok(catsDTO);
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var catsDTO = await _catService.GetCategoriesProducts();

            if (catsDTO == null)
                return NotFound("Categorias e produtos não encontrados");

            return Ok(catsDTO);
        }

        [HttpGet("{id}", Name ="GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var catDTO = await _catService.GetCatById(id);

            if (catDTO == null)
                return NotFound("Categoria não encontrada");

            return Ok(catDTO);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Post([FromBody] CategoryDTO catDTO)
        {
            if (catDTO == null)
                return BadRequest("Dados inválidos");

            await _catService.AddCat(catDTO);
            return new CreatedAtRouteResult("Category", new { id = catDTO.CategoryId, catDTO });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO catDTO)
        {
            if (id != catDTO.CategoryId)
                return NotFound("Categoria não encontrada");
            if (catDTO is null)
                return BadRequest();

            await _catService.UpdateCat(catDTO);
            return Ok(catDTO);
        }

        [HttpDelete("{id}")]
	    [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            var catDTO = await _catService.GetCatById(id);

            if (catDTO == null)
                return NotFound("Categoria não encontrada");

            await _catService.RemoveCat(id);
            return Ok(catDTO);
        }
    }
}
