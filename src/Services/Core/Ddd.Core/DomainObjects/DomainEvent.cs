using Ddd.Core.Messages;

namespace Ddd.Core.DomainObjects
{
	public class DomainEvent : Event
	{
		public DomainEvent(Guid aggregateId)
		{
			base.AggregateId = aggregateId;
		}
	}
}
