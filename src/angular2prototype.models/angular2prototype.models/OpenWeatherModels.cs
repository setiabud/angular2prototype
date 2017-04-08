using System;
using System.Collections.Generic;
using System.Net;

namespace angular2prototype.models
{
	public class OpenWeatherResponse
	{
		public string Name { get; set; }

		public IEnumerable<WeatherDescription> Weather { get; set; }

		public Main Main { get; set; }
	}

	public class WeatherDescription
	{
		public string Main { get; set; }
		public string Description { get; set; }
	}

	public class Main
	{
		public string Temp { get; set; }
	}

	public class WeatherResponse : ServiceResponse
	{		
		public string Temp { get; set; }
		public string Summary { get; set; }
		public string City { get; set; }
	}

	public class ServiceResponse
	{
		public HttpStatusCode Status { get; set; }
		public string Message { get; set; }
	}
}
