using Ddd.Core.DomainObjects;

namespace Ddd.Catalog.Domain;

public class Category : Entity
{
	public string Name { get; private set; }
	public int Code { get; private set; }
	// EF Only
	public ICollection<Product> Products { get; set; }

	protected Category()
	{
		Name = string.Empty;
		Code = 0;
		Products = new List<Product>();
	}

	public Category(string name, int code)
	{
		Name = name;
		Code = code;
		Products = new List<Product>();
		Validate();
	}

	public override string ToString()
	{
		return $"{Name} [{Code}]";
	}

	public void Validate()
	{
		AssertionConcern.ValidateIfEmpty(Name, "Category Name cannot be empty");
		AssertionConcern.ValidateIfLessThanOrEqual(Code, 0, "Category Code must be greater than zero");
	}
}
