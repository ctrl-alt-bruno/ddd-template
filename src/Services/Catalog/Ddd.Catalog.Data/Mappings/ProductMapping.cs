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

			builder.Property(c => c.Active)
				.IsRequired();

			builder.Property(c => c.Price)
				.IsRequired()
				.HasColumnType("decimal(18,2)");

			builder.Property(c => c.CreateDate)
				.IsRequired();

			builder.Property(c => c.StockQuantity)
				.HasColumnType("int");

			builder.OwnsOne(c => c.Dimensions, cm =>
			{
				cm.Property(c => c.Height)
					.HasColumnName("Height")
					.HasColumnType("decimal(18,2)");

				cm.Property(c => c.Width)
					.HasColumnName("Width")
					.HasColumnType("decimal(18,2)");

				cm.Property(c => c.Depth)
					.HasColumnName("Depth")
					.HasColumnType("decimal(18,2)");
			});

			// N : 1 => Produtos possuem Categoria
			builder.HasOne(c => c.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(c => c.CategoryId);

			builder.ToTable("Products");
		}
	}
}
