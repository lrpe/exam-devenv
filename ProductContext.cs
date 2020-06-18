using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DevEnvExam
{
	public class ProductContext : DbContext
	{
		private readonly IConfiguration configuration;

		public ProductContext(IConfiguration configuration)
			=> this.configuration = configuration;

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
				.UseSqlite(configuration.GetConnectionString("SQLite"));

		protected override void OnModelCreating(ModelBuilder modelBuilder)
			=> modelBuilder
				.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

	}
}
