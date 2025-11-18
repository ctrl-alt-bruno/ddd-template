namespace Ddd.Core.Data
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}
