namespace Ddd.Catalog.Domain
{
	public interface IStockService : IDisposable
	{
		Task<bool> ReduceStock(Guid productId, int quantity);
		Task<bool> IncreaseStock(Guid productId, int quantity);
	}
}
