using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShopSchool.ProductAPI.DTOs;
using VShopSchool.ProductAPI.Interfaces;

namespace VShopSchool.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _prodService;
        public ProductsController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var prodsDTO = await _prodService.GetProducts();

            if (prodsDTO is null)
                return NotFound("Produtos não encontrado");

            return Ok(prodsDTO);
        }

        [HttpGet("{id}", Name ="GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var prodDTO = await _prodService.GetProdById(id);

            if (id != prodDTO.Id)
                return NotFound("Produto não encontrado");
            if (prodDTO is null)
                return BadRequest();

            return Ok(prodDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO prodDTO)
        {
            if (prodDTO == null)
                return BadRequest("Dados inválidos");

            await _prodService.AddProd(prodDTO);
            return new CreatedAtRouteResult("Product", new { id = prodDTO.Id, prodDTO });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO prodDTO)
        {
            if (id == prodDTO.Id)
                return NotFound("Produto não encontrado");
            if (prodDTO is null)
                return BadRequest();

            await _prodService.UpdateProd(prodDTO);
            return Ok("Produto Atualizado /n" + prodDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var prodDTO = await _prodService.GetProdById(id);

            if (prodDTO == null)
                return NotFound("Produto não encontrado");

            await _prodService.RemoveProd(id);
            return Ok("Produto Removido /n" + prodDTO);
        }
    }
}
