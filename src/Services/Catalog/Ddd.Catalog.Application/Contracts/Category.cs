using System.ComponentModel.DataAnnotations;

namespace Ddd.Catalog.Application.Contracts
{
	public class Category
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public required string Name { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(1, int.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
		public int Code { get; set; }
	}
}
