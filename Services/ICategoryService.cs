using DevEnvExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetAll();
		Task<Category> Get(int id);
		Task<Category> Create(Category category);
		Task<Category> Update(Category category);
		Task<Category> Delete(int id);
	}
}
