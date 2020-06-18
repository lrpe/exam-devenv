using DevEnvExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAll();
		Task<IEnumerable<Product>> GetByCategory(int categoryId);
		Task<Product> Get(int id);
		Task<Product> Create(Product product);
		Task<Product> Update(Product product);
		Task<Product> Delete(int id);
	}
}
