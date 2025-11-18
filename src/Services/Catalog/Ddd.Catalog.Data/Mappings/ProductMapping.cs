using Ddd.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Catalog.Data.Mappings
{
	public class ProductMapping : IEntityTypeConfiguration<Product>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasColumnType("varchar(255)");

			builder.Property(c => c.Description)
				.IsRequired()
				.HasColumnType("varchar(510)");

			builder.Property(c => c.Thumbnail)
				.IsRequired()
				.HasColumnType("varchar(255)");

			builder.OwnsOne(c => c.Dimensions, cm =>
			{
				cm.Property(c => c.Height)
					.HasColumnName("Height")
					.HasColumnType("int");

				cm.Property(c => c.Width)
					.HasColumnName("Width")
					.HasColumnType("int");

				cm.Property(c => c.Depth)
					.HasColumnName("Depth")
					.HasColumnType("int");
			});

			builder.ToTable("Products");
		}
	}
}
