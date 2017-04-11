using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using angular2prototype.services;
using System.Threading.Tasks;
using angular2prototype.web.Models;
using angular2prototype.core.services;

namespace angular2prototype.web.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		private readonly IValueService _valueService;

		public ValuesController(IValueService valueService)
		{
			_valueService = valueService;
		}

		// GET api/values
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery, Required]SearchOptions searchOptions)
		{
			// Dummy search results - this would normally be replaced by another service call, perhaps to a database
			var searchResults = await _valueService.Search(searchOptions.Name);
			var formattedResult = JsonConvert.SerializeObject(searchResults, Formatting.Indented);

			Response.Headers.Add("x-total-count", searchResults.Count.ToString());

			return Ok(formattedResult);
		}

		// GET api/values/5
		[HttpGet("{id:int:min(1)}", Name = "GetById")]
		public async Task<IActionResult> GetById([Required]int id)
		{
			try
			{
				// Dummy search result - this would normally be replaced by another service call, perhaps to a database
				var customObject = await _valueService.Get(id);

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
		public async Task<IActionResult> Post([FromBody]NewValuesViewModel value)
		{
			if (!ModelState.IsValid) { return BadRequest(ModelState); }
			var createdObject = await _valueService.Add(new models.ValueModel { Name = value.Name });
			return Created($"api/Values/", value.Name);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put([FromBody]ValuesViewModel value)
		{
			try
			{
				if (!ModelState.IsValid) { return BadRequest(ModelState); }

				var customObject = await _valueService.Get(value.Id);
				customObject.Id = value.Id;
				customObject.Name = value.Name;
				await _valueService.Update(customObject);

				return Accepted();
			}
			catch (KeyNotFoundException)
			{
				Response.Headers.Add("x-status-reason", $"No resource was found with the unique identifier '{value.Id}'.");
				return NotFound();
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var customObject = await _valueService.Get(id);
				await _valueService.Delete(id);
				return NoContent();
			}
			catch (KeyNotFoundException)
			{
				Response.Headers.Add("x-status-reason", $"No resource was found with the unique identifier '{id}'.");
				return NotFound();
			}
		}
	}

	// This should go into its own separate file - included here for simplicity
	public class SearchOptions
	{
		public string Name { get; set; }
	}
}
