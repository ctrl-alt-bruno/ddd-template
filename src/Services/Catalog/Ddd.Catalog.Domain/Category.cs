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

	}

	public Category(string name, int code)
	{
		Name = name;
		Code = code;
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
