using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public class ProductService : IProductService
	{
		private readonly ProductContext context;

		public ProductService(ProductContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await context.Products.ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetByCategory(int categoryId)
		{
			return await context.Products
				.Where(p => p.CategoryId == categoryId)
				.ToListAsync();
		}

		public async Task<Product> Get(int id)
		{
			return await context.Products.FindAsync(id);
		}

		public async Task<Product> Create(Product product)
		{
			context.Products.Add(product);
			await context.SaveChangesAsync();
			return product;
		}

		public async Task<Product> Update(Product product)
		{
			if (!ProductExists(product.Id)) return null;
			context.Products.Update(product);
			await context.SaveChangesAsync();
			return product;
		}

		public async Task<Product> Delete(int id)
		{
			var product = await context.Products.FindAsync(id);
			if (product != null)
			{
				context.Products.Remove(product);
				await context.SaveChangesAsync();
			}
			return product;
		}

		private bool ProductExists(int id)
		{
			return context.Products.Any(c => c.Id == id);
		}
	}
}
