using Ddd.Core.DomainObjects;

namespace Ddd.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public bool Active { get; private set; }
	public decimal Price { get; private set; }
	public DateTime CreateDate { get; private set; }
	public string Thumbnail { get; private set; }
	public int? StockQuantity { get; private set; }
	public Guid CategoryId { get; private set; }
	public Dimensions? Dimensions { get; set; }
	public Category? Category { get; private set; }

	public Product()
	{
		Name = string.Empty;
		Description = string.Empty;
		Active = false;
		Price = 0;
		CreateDate = DateTime.MinValue;
		Thumbnail = string.Empty;
		StockQuantity = null;
		CategoryId = Guid.NewGuid();
		Dimensions = null;
		Category = null;
	}

	public Product(string name, string description, bool active, decimal price, DateTime createDate, string thumbnail,
		Guid categoryId, Dimensions dimensions)
	{
		Name = name;
		Description = description;
		Active = active;
		Price = price;
		CreateDate = createDate;
		Thumbnail = thumbnail;
		CategoryId = categoryId;
		Dimensions = dimensions;
		Validate();
	}

	public void Activate()
	{
		Active = true;
	}

	public void Deactivate()
	{
		Active = false;
	}

	public void ChangeCategory(Category category)
	{
		Category = category;
		CategoryId = category.Id;
	}

	public void ChangeDescription(string description)
	{
		AssertionConcern.ValidateIfEmpty(description, "Product Description cannot be empty");
		Description = description;
	}

	public void ReduceStock(int quantity)
	{
		if (quantity < 0)
			quantity *= -1;

		if (!CheckStockQuantity(quantity))
			throw new DomainException("Stock quantity is not enough");

		StockQuantity -= quantity;
	}

	public void IncreaseStock(int quantity)
	{
		if (quantity < 0)
			quantity *= -1;

		StockQuantity += quantity;
	}

	public bool CheckStockQuantity(int quantity)
	{
		return StockQuantity >= quantity;
	}

	public void Validate()
	{
		AssertionConcern.ValidateIfEmpty(Name, "Product Name cannot be empty");
		AssertionConcern.ValidateIfEmpty(Description, "Product Description cannot be empty");
		AssertionConcern.ValidateIfEmpty(Thumbnail, "Product Thumbnail cannot be empty");
		AssertionConcern.ValidateIfDifferent(CategoryId, Guid.Empty, "Product Category cannot be empty");
		AssertionConcern.ValidateIfLessThan(Price, 0, "Product Price cannot be negative");
	}
}
