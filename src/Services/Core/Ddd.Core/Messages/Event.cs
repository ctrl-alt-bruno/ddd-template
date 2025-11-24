using MediatR;

namespace Ddd.Core.Messages
{
	public abstract class Event : Message, INotification
	{
		public DateTime TimeStamp { get; private set; }

		protected Event()
		{
			TimeStamp = DateTime.UtcNow;
		}
	}
}
