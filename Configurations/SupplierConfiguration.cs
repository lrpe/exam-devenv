using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevEnvExam.Configurations
{
	public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
	{
		public void Configure(EntityTypeBuilder<Supplier> builder)
		{
			builder.Property(c => c.Name)
				.HasDefaultValue("Unnamed Supplier")
				.IsRequired();
		}
	}
}
