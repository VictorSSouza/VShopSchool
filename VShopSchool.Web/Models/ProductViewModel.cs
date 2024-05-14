using System.ComponentModel.DataAnnotations;

namespace VShopSchool.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
	    [Display(Name="Nome")]
        public string? Name { get; set; }

        [Required]
	    [Display(Name="Preço")]
        public decimal Price { get; set; }

	    [Display(Name="Descrição")]
        public string? Description { get; set; }

        [Required]
	    [Display(Name="Quantidade em estoque")]
        public long Stock { get; set; }

        [Required]
	    [Display(Name="Url da imagem")]
        public string? ImageUrl { get; set; }

        [Display(Name="Nome da categoria")]
        public string? CategoryName { get; set; }

	    [Display(Name="Categorias")]
        public int CategoryId { get; set; }
    }
}
