using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é Obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O preço é Obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [Range(1, 9999)]
        public long Stock { get; set; }
        public string? ImageUrl { get; set; }

	    [JsonIgnore]
        public Category? Categories { get; set; }
        public int CategoryId { get; set; }

    }
}
