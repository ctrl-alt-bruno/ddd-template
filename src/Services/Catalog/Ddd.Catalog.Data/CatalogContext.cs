using Ddd.Catalog.Domain;
using Ddd.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Catalog.Data
{
	public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options), IUnitOfWork
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Aplicar todas as configurações de mapeamento do assembly atual
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			// Configuração global: todas as strings como varchar(100)
			configurationBuilder
				.Properties<string>()
				.HaveMaxLength(100)
				.AreUnicode(false);
		}

		public async Task<bool> Commit()
		{
			return await SaveChangesAsync() > 0;
		}
	}
}
