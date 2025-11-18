using Ddd.Catalog.Domain;
using Ddd.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Catalog.Data
{
	public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options), IUnitOfWork
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			// Configuração global: todas as strings como varchar(100)
			configurationBuilder
				.Properties<string>()
				.HaveMaxLength(100)
				.AreUnicode(false);
		}

		public Task<bool> Commit()
		{
			throw new NotImplementedException();
		}
	}
}
