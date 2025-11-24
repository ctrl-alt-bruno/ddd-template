using Ddd.Core.Messages;

namespace Ddd.Core.Bus
{
	public interface IMediatrHandler
	{
		Task PublishEvent<T>(T anEvent) where T : Event;
	}
}
