using DevEnvExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEnvExam.Services
{
	public interface ISupplierService
	{
		Task<IEnumerable<Supplier>> GetAll();
		Task<Supplier> Get(int id);
		Task<Supplier> Create(Supplier supplier);
		Task<Supplier> Update(Supplier supplier);
		Task<Supplier> Delete(int id);
	}
}
