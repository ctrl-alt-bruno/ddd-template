using Ddd.Core.DomainObjects;

namespace Ddd.Catalog.Domain.Events
{
	public class ProductStockLowEvent : DomainEvent
	{
		public int QuantityLeft { get; private set; }

		public ProductStockLowEvent(Guid aggregateId, int quantityLeft) : base(aggregateId)
		{
			QuantityLeft = quantityLeft;
		}
	}
}
