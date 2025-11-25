using System.ComponentModel.DataAnnotations;

namespace Ddd.Catalog.Application.Contracts
{
	public class Product
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public Guid CategoryId { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public required string Name { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public required string Description { get; set; }

		public bool Active { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
		public decimal Price { get; set; }

		public DateTime CreateDate { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public required string Thumbnail { get; set; }

		public int? StockQuantity { get; set; }

		public Dimensions? Dimensions { get; set; }
	}

	public class Dimensions
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
		public decimal Height { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
		public decimal Width { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
		public decimal Depth { get; set; }
	}
}
