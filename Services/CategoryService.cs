using DevEnvExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ProductContext context;

		public CategoryService(ProductContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<Category> Get(int id)
		{
			return await context.Categories.FindAsync(id);
		}

		public async Task<Category> Create(Category category)
		{
			context.Categories.Add(category);
			await context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> Update(Category category)
		{
			if (!CategoryExists(category.Id)) return null;
			context.Categories.Update(category);
			await context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> Delete(int id)
		{
			var category = await context.Categories.FindAsync(id);
			if (category != null)
			{
				context.Categories.Remove(category);
				await context.SaveChangesAsync();
			}
			return category;
		}

		private bool CategoryExists(int id)
		{
			return context.Categories.Any(c => c.Id == id);
		}
	}
}
