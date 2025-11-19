using Ddd.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ddd.Catalog.Data.Mappings
{
	public class CategoryMapping : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasColumnType("varchar(255)");

			builder.Property(x => x.Code)
				.IsRequired();

			// 1 : N => Categoria possui Produtos
			builder.HasMany(x => x.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId);

			builder.ToTable("Categories");
		}
	}
}
