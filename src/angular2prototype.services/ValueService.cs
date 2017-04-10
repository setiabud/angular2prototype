using angular2prototype.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2prototype.services
{
	public class ValueService : IValueService
	{
		private List<ValueModel> _values;
		public ValueService()
		{
			_values = new List<ValueModel>() {
										new ValueModel { Id = 1, Name = "value 1" },
										new ValueModel { Id = 2, Name = "value 2" }
									 };
		}

		public async Task<List<ValueModel>> Search(string name)
		{
			return await Task.Run(() => _values.Where(v => v.Name.Contains(name)).ToList());
		}

		public async Task<ValueModel> Get(int id)
		{
			return await Task.Run(() => _values.Find(v => v.Id == id));
		}

		public async Task Update(ValueModel value)
		{
			if (await IdExist(value.Id))
			{
				await Task.Run(() => { _values.Find(v => v.Id == value.Id).Name = value.Name; }); return;
			}
			throw new KeyNotFoundException($"Id '{value.Id}' is not found.");
		}

		public async Task Delete(int id)
		{
			if (await IdExist(id))
			{
				await Task.Run(() => { _values.RemoveAll(v => v.Id == id); }); return;
			}
			throw new KeyNotFoundException($"Id '{id}' is not found.");
		}

		public async Task<ValueModel> Add(ValueModel value)
		{
			return await Task.Run(() =>
			{
				var maxId = _values.Max(v => v.Id);
				var newItem = new ValueModel { Id = maxId + 1, Name = value.Name };
				_values.Add(newItem);
				return newItem;
			});
		}

		private async Task<bool> IdExist(int id)
		{
			var item = await Get(id);
			return item != null;
		}
	}
}
