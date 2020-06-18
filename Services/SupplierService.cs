using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public class SupplierService : ISupplierService
	{
		private readonly ProductContext context;

		public SupplierService(ProductContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Supplier>> GetAll()
		{
			return await context.Suppliers.ToListAsync();
		}

		public async Task<Supplier> Get(int id)
		{
			return await context.Suppliers.FindAsync(id);
		}

		public async Task<Supplier> Create(Supplier supplier)
		{
			context.Suppliers.Add(supplier);
			await context.SaveChangesAsync();
			return supplier;
		}

		public async Task<Supplier> Update(Supplier supplier)
		{
			if (!SupplierExists(supplier.Id)) return null;
			context.Suppliers.Update(supplier);
			await context.SaveChangesAsync();
			return supplier;
		}

		public async Task<Supplier> Delete(int id)
		{
			var supplier = await context.Suppliers.FindAsync(id);
			if (supplier != null)
			{
				context.Suppliers.Remove(supplier);
				await context.SaveChangesAsync();
			}
			return supplier;
		}

		private bool SupplierExists(int id)
		{
			return context.Suppliers.Any(c => c.Id == id);
		}
	}
}
