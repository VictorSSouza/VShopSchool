using Microsoft.AspNetCore.Mvc;
using VShopSchool.Web.Models;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        //private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService)
        {
                _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await _productService.GetAllProducts();

            if (result is null)
                return View("Error");

            return View(result);
        }
    }
}
