using Ddd.Core.DomainObjects;

namespace Ddd.Core.Data
{
	public interface IRepository<T> : IDisposable where T : IAggregateRoot
	{
		IUnitOfWork UnitOfWork { get; }
	}
}
