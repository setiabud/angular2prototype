using System;
using System.Collections.Generic;
using System.Text;

namespace angular2prototype.core.models
{
	public interface IValueModel
	{
		int Id { get; set; }

		string Name { get; set; }
	}
}
