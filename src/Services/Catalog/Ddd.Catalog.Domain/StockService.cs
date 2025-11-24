
using Ddd.Catalog.Domain.Events;
using Ddd.Core;
using Ddd.Core.Bus;

namespace Ddd.Catalog.Domain
{
	public class StockService(IProductRepository productRepository, IMediatrHandler bus) : IStockService
	{
		public async Task<bool> IncreaseStock(Guid productId, int quantity)
		{
			Product product = await productRepository.GetById(productId);

			if (product == null)
				return false;

			product.IncreaseStock(quantity);

			productRepository.Update(product);

			return await productRepository.UnitOfWork.Commit();
		}

		public async Task<bool> ReduceStock(Guid productId, int quantity)
		{
			Product product = await productRepository.GetById(productId);

			if (product == null)
				return false;

			if (!product.CheckStockQuantity(quantity))
				return false;

			product.ReduceStock(quantity);

			if (product.StockQuantity < 10)
				await bus.PublishEvent(new ProductStockLowEvent(product.Id, product.StockQuantity.Value));

			productRepository.Update(product);

			return await productRepository.UnitOfWork.Commit();
		}

		public void Dispose()
		{
			productRepository.Dispose();
		}
	}
}
