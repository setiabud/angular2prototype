using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace angular2prototype.core.models
{
	public interface IOpenWeatherResponse
	{
		string Name { get; set; }

		IEnumerable<IWeatherDescription> Weather { get; set; }

		IMain Main { get; set; }
	}

	public interface IWeatherDescription
	{
		string Main { get; set; }
		string Description { get; set; }
	}

	public interface IMain
	{
		string Temp { get; set; }
	}

	public interface IWeatherResponse : IServiceResponse
	{
		string Temp { get; set; }
		string Summary { get; set; }
		string City { get; set; }
	}

	public interface IServiceResponse
	{
		HttpStatusCode Status { get; set; }
		string Message { get; set; }
	}
}
