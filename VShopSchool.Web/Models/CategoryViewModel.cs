using System.ComponentModel.DataAnnotations;

namespace VShopSchool.Web.Models
{
	public class CategoryViewModel
	{
		public int CategoryId { get; set; }

		[Required]
		[Display(Name="Nome")]
		public string? Name { get; set; }
	}
}
