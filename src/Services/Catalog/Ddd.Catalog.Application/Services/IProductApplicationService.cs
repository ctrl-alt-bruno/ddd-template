using Ddd.Catalog.Application.Contracts;
using ContractProduct = Ddd.Catalog.Application.Contracts.Product;
using ContractCategory = Ddd.Catalog.Application.Contracts.Category;

namespace Ddd.Catalog.Application.Services
{
    public interface IProductApplicationService : IDisposable
    {
        // Stock operations (domain service)
        Task<ProductStockResult> IncreaseStock(Guid productId, int quantity);
        Task<ProductStockResult> ReduceStock(Guid productId, int quantity);

        // Product queries
        Task<ContractProduct?> GetById(Guid productId);
        Task<IEnumerable<ContractProduct>> GetAll();
        Task<IEnumerable<ContractProduct>> GetByCategoryId(int categoryId);

        // Product commands
        Task<bool> Add(ContractProduct product);
        Task<bool> Update(ContractProduct product);

        // Category operations
        Task<IEnumerable<ContractCategory>> GetCategories();
        Task<bool> AddCategory(ContractCategory category);
        Task<bool> UpdateCategory(ContractCategory category);
    }
}