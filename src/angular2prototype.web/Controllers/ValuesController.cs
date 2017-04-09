using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace angular2prototype.web.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IActionResult Get([FromQuery, Required]SearchOptions searchOptions)
		{
			// Dummy search results - this would normally be replaced by another service call, perhaps to a database
			var searchResults = new[]{
										new{ id=1, Name="value 1" },
										new{ id=2, Name="value 2"}
									 }.ToList();

			var formattedResult = JsonConvert.SerializeObject(searchResults, Formatting.Indented);

			Response.Headers.Add("x-total-count", searchResults.Count.ToString());

			return Ok(formattedResult);
		}

		// GET api/values/5
		[HttpGet("{id:int:min(1)}", Name = "GetById")]
		public IActionResult GetById([Required]int id)
		{
			try
			{
				// Dummy search result - this would normally be replaced by another service call, perhaps to a database
				var customObject = new { id = id, name = "name" };

				var formattedCustomObject = JsonConvert.SerializeObject(customObject, Formatting.Indented);

				return Ok(formattedCustomObject);
			}
			catch (KeyNotFoundException)
			{
				Response.Headers.Add("x-status-reason", $"No resource was found with the unique identifier '{id}'.");
				return NotFound();
			}
		}

		[HttpGet("{name}")]
		public IActionResult GetByName([Required]string name)
		{
			Response.Headers.Add("x-status-reason", $"The value '{name}' is not recognised as a valid integer to uniquely identify a resource.");
			return BadRequest();
		}

		// POST api/values
		[HttpPost]
		public IActionResult Post([FromBody]string value)
		{
			return Created($"api/Values/{value}", value);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]string value)
		{
			return Accepted(value);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return NoContent();
		}
	}

	// This should go into its own separate file - included here for simplicity
	public class SearchOptions
	{
		public string Name { get; set; }
	}
}
