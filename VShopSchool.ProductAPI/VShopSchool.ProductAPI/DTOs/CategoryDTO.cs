using System.ComponentModel.DataAnnotations;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
