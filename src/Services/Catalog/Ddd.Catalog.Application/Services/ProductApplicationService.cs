using Ddd.Catalog.Application.Contracts;
using Ddd.Catalog.Domain;
using DomainProduct = Ddd.Catalog.Domain.Product;
using DomainCategory = Ddd.Catalog.Domain.Category;
using DomainDimensions = Ddd.Catalog.Domain.Dimensions;
using ContractProduct = Ddd.Catalog.Application.Contracts.Product;
using ContractCategory = Ddd.Catalog.Application.Contracts.Category;
using ContractDimensions = Ddd.Catalog.Application.Contracts.Dimensions;
using System.Reflection;

namespace Ddd.Catalog.Application.Services
{
	public class ProductApplicationService(IStockService stockService, IProductRepository productRepository) : IProductApplicationService
	{
		public async Task<ProductStockResult> IncreaseStock(Guid productId, int quantity)
		{
			if (quantity <= 0)
				return ProductStockResult.FailureResult("Quantity must be greater than zero");

			bool result = await stockService.IncreaseStock(productId, quantity);

			if (!result)
				return ProductStockResult.FailureResult("Product not found or failed to increase stock");

			DomainProduct product = await productRepository.GetById(productId);
			return ProductStockResult.SuccessResult(
				product?.StockQuantity ?? 0,
				$"Stock increased by {quantity} units");
		}

		public async Task<ProductStockResult> ReduceStock(Guid productId, int quantity)
		{
			if (quantity <= 0)
				return ProductStockResult.FailureResult("Quantity must be greater than zero");

			// Delegate to domain service - it handles all business rules
			bool result = await stockService.ReduceStock(productId, quantity);

			if (!result)
				return ProductStockResult.FailureResult("Failed to reduce stock. Product not found or insufficient stock.");

			DomainProduct product = await productRepository.GetById(productId);
			return ProductStockResult.SuccessResult(
				product?.StockQuantity ?? 0,
				$"Stock reduced by {quantity} units");
		}

		public async Task<ContractProduct?> GetById(Guid productId)
		{
			DomainProduct domainProduct = await productRepository.GetById(productId);

			if (domainProduct == null)
				return null;

			return MapToContract(domainProduct);
		}

		public async Task<IEnumerable<ContractProduct>> GetAll()
		{
			IEnumerable<DomainProduct> products = await productRepository.GetAll();
			return products.Select(MapToContract);
		}

		public async Task<IEnumerable<ContractProduct>> GetByCategoryId(int categoryId)
		{
			IEnumerable<DomainProduct> products = await productRepository.GetByCategoryId(categoryId);
			return products.Select(MapToContract);
		}

		public async Task<bool> Add(ContractProduct product)
		{
			DomainProduct domainProduct = MapToDomain(product);
			productRepository.Add(domainProduct);
			return await productRepository.UnitOfWork.Commit();
		}

		public async Task<bool> Update(ContractProduct product)
		{
			// Fetch existing product to apply business rules
			DomainProduct existingProduct = await productRepository.GetById(product.Id);

			if (existingProduct == null)
				return false;

			// Apply changes using domain methods (business rules)
			if (existingProduct.Description != product.Description)
				existingProduct.ChangeDescription(product.Description);

			if (existingProduct.Active != product.Active)
			{
				if (product.Active)
					existingProduct.Activate();
				else
					existingProduct.Deactivate();
			}

			// Note: Stock changes should go through StockService, not here
			// Note: Name, Price, Thumbnail, CreateDate, Dimensions updates
			// would need specific domain methods if they have business rules

			productRepository.Update(existingProduct);
			return await productRepository.UnitOfWork.Commit();
		}

		public async Task<IEnumerable<ContractCategory>> GetCategories()
		{
			IEnumerable<DomainCategory> categories = await productRepository.GetCategories();
			return categories.Select(MapToContract);
		}

		public async Task<bool> AddCategory(ContractCategory category)
		{
			DomainCategory domainCategory = MapToDomain(category);
			productRepository.Add(domainCategory);
			return await productRepository.UnitOfWork.Commit();
		}

		public async Task<bool> UpdateCategory(ContractCategory category)
		{
			DomainCategory domainCategory = MapToDomain(category);
			productRepository.Update(domainCategory);
			return await productRepository.UnitOfWork.Commit();
		}

		// Mapping methods
		private ContractProduct MapToContract(DomainProduct domainProduct)
		{
			return new ContractProduct
			{
				Id = domainProduct.Id,
				Name = domainProduct.Name,
				Description = domainProduct.Description,
				Active = domainProduct.Active,
				Price = domainProduct.Price,
				CreateDate = domainProduct.CreateDate,
				Thumbnail = domainProduct.Thumbnail,
				StockQuantity = domainProduct.StockQuantity,
				CategoryId = domainProduct.CategoryId,
				Dimensions = domainProduct.Dimensions != null
					? new ContractDimensions
					{
						Height = domainProduct.Dimensions.Height,
						Width = domainProduct.Dimensions.Width,
						Depth = domainProduct.Dimensions.Depth
					}
					: null
			};
		}

		private DomainProduct MapToDomain(ContractProduct contractProduct)
		{
			DomainDimensions? dimensions = contractProduct.Dimensions != null
				? new DomainDimensions(
					contractProduct.Dimensions.Height,
					contractProduct.Dimensions.Width,
					contractProduct.Dimensions.Depth)
				: null;

			var product = new DomainProduct(
				contractProduct.Name,
				contractProduct.Description,
				contractProduct.Active,
				contractProduct.Price,
				contractProduct.CreateDate,
				contractProduct.Thumbnail,
				contractProduct.CategoryId,
				dimensions!);

			// Note: Stock quantity is NOT set here - use StockService for stock operations
			// Stock changes involve business rules (validation, events) and must go through domain service

			return product;
		}

		private ContractCategory MapToContract(DomainCategory domainCategory)
		{
			return new ContractCategory
			{
				Id = domainCategory.Id,
				Name = domainCategory.Name,
				Code = domainCategory.Code
			};
		}

		private DomainCategory MapToDomain(ContractCategory contractCategory)
		{
			var category = new DomainCategory(contractCategory.Name, contractCategory.Code);

			// Use reflection to set the Id since it's inherited from Entity
			PropertyInfo? idProperty = typeof(DomainCategory).BaseType?.GetProperty("Id");
			idProperty?.SetValue(category, contractCategory.Id);

			return category;
		}

		public void Dispose()
		{
			stockService?.Dispose();
			productRepository?.Dispose();
		}
	}
}
