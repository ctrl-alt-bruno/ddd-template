using Ddd.Core.Data;

namespace Ddd.Catalog.Domain
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetAll();
		Task<Product> GetById(Guid id);
		Task<IEnumerable<Product>> GetByCategoryId(int categoryId);
		Task<IEnumerable<Category>> GetCategories();
		void Add(Product product);
		void Update(Product product);
		void Add(Category category);
		void Update(Category category);
	}
}
