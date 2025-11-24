using MediatR;

namespace Ddd.Catalog.Domain.Events
{
	public class ProductEventHandler(IProductRepository productRepository) : INotificationHandler<ProductStockLowEvent>
	{
		public async Task Handle(ProductStockLowEvent notification, CancellationToken cancellationToken)
		{
			Product product = await productRepository.GetById(notification.AggregateId);

			// TODO: send email do interested parts
		}
	}
}
