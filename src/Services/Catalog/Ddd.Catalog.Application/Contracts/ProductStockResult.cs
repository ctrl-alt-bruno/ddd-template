namespace Ddd.Catalog.Application.Contracts
{
	public class ProductStockResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public int? StockQuantity { get; set; }

		public ProductStockResult(bool success, string message, int? stockQuantity = null)
		{
			Success = success;
			Message = message;
			StockQuantity = stockQuantity;
		}

		public static ProductStockResult SuccessResult(int stockQuantity, string message = "Operation completed successfully")
		{
			return new ProductStockResult(true, message, stockQuantity);
		}

		public static ProductStockResult FailureResult(string message)
		{
			return new ProductStockResult(false, message);
		}
	}
}
