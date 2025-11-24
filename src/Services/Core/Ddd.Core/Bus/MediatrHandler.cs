using Ddd.Core.Messages;

namespace Ddd.Core.Bus
{
	public class MediatrHandler(IMediatrHandler mediatrHandler) : IMediatrHandler
	{
		public async Task PublishEvent<T>(T anEvent) where T : Event
		{
			await mediatrHandler.PublishEvent(anEvent);
		}
	}
}
