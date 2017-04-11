using angular2prototype.core.models;
using angular2prototype.core.services;
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
		private List<IValueModel> _values;
		public ValueService()
		{
			_values = new List<ValueModel>() {
										new ValueModel { Id = 1, Name = "value 1" },
										new ValueModel { Id = 2, Name = "value 2" }
									 }.Cast<IValueModel>().ToList();
		}

		public async Task<List<IValueModel>> Search(string name)
		{
			return await Task.Run(() => _values.Cast<IValueModel>().Where(v => v.Name.Contains(name)).ToList());
		}

		public async Task<IValueModel> Get(int id)
		{
			var result = await Task.Run(() => _values.Find(v => v.Id == id));
			if (result == null) throw new KeyNotFoundException($"Id '{id}' is not found.");
			return result;
		}

		public async Task Update(IValueModel value)
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

		public async Task<IValueModel> Add(IValueModel value)
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
