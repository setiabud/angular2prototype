using angular2prototype.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace angular2prototype.services
{
    public class ValueService : IValueService
	{
		public async Task<List<ValueModel>> Search(string name)
		{
			// Dummy search results - this would normally be replaced by another service call, perhaps to a database
			var searchResults = new List<ValueModel> () { 
										new ValueModel { Id = 1, Name = "value 1" },
										new ValueModel { Id = 2, Name = "value 2" }
									 };
			return await Task.Run(() => searchResults);
		}

		public async Task<ValueModel> Get(int id)
		{
			// Dummy search result - this would normally be replaced by another service call, perhaps to a database
			var customObject = new ValueModel { Id = id, Name = "name" };
			return await Task.Run(() => customObject);
		}

		public async Task Update(ValueModel value)
		{
			await Task.Run(() => value);
		}

		public async Task Delete(int id)
		{
			await Task.Run(() => id);
		}

		public async Task<ValueModel> Add(ValueModel value)
		{
			return await Task.Run(() => value);
		}
	}
}
