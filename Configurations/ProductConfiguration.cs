using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevEnvExam.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(c => c.Name)
				.HasDefaultValue("Unnamed Product")
				.IsRequired();

			builder.Property(c => c.Price)
				.IsRequired();
		}
	}
}
