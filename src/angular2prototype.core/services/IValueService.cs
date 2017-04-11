using System.Collections.Generic;
using System.Threading.Tasks;
using angular2prototype.core.models;

namespace angular2prototype.core.services
{
	public interface IValueService
	{
		Task<IValueModel> Add(IValueModel value);
		Task Delete(int id);
		Task<IValueModel> Get(int id);
		Task<List<IValueModel>> Search(string name);
		Task Update(IValueModel value);
	}
}