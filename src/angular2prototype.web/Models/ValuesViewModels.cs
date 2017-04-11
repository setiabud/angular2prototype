using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angular2prototype.web.Models
{
    public class NewValuesViewModel
    {
		[Required]
		public string Name { get; set; }
	}

	public class ValuesViewModel 
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
