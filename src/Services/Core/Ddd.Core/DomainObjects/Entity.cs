namespace Ddd.Core.DomainObjects;

public abstract class Entity
{
	public Guid Id { get; set; }

	protected Entity()
	{
		Id = Guid.NewGuid();
	}

	public override bool Equals(object? obj)
	{
		Entity? compareTo = obj as Entity;

		if (ReferenceEquals(this, compareTo))
			return true;

		if (ReferenceEquals(null, compareTo))
			return false;

		return Id.Equals(compareTo.Id);
	}

	private bool Equals(Entity other)
	{
		return Id.Equals(other.Id);
	}

	public override int GetHashCode()
	{
		return (GetType().GetHashCode() * 666) + Id.GetHashCode();
	}

	public override string ToString()
	{
		return $"{GetType().Name} [Id={Id}]";
	}

	public static bool operator ==(Entity? left, Entity? right)
	{
		if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
			return true;

		if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
			return false;

		return left.Equals(right);
	}

	public static bool operator !=(Entity? left, Entity? right)
	{
		return !(left == right);
	}
}
