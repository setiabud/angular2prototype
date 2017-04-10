using System.Collections.Generic;
using System.Threading.Tasks;
using angular2prototype.models;

namespace angular2prototype.services
{
	public interface IValueService
	{
		Task<ValueModel> Add(ValueModel value);
		Task Delete(int id);
		Task<ValueModel> Get(int id);
		Task<List<ValueModel>> Search(string name);
		Task Update(ValueModel value);
	}
}