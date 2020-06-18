using AutoMapper;
using DevEnvExam.Models;
using DevEnvExam.Resources;
using DevEnvExam.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEnvExam.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService service;
		private readonly IMapper mapper;

		public CategoriesController(ICategoryService service, IMapper mapper)
		{
			this.service = service;
			this.mapper = mapper;
		}

		/// <summary>Lists all categories</summary>
		/// <returns>A list of all categories</returns>
		/// <response code="200">Returns all categories</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CategoryResource>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<CategoryResource>>> GetAll()
		{
			var categories = await service.GetAll();
			var categoryResources = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
			return Ok(categoryResources);
		}

		/// <summary>Get a single category</summary>
		/// <returns>A single category</returns>
		/// <response code="200">Returns the category with the given id</response>
		/// <response code="404">No category exists with the given id</response>
		/// <param name="id">The id of the category</param>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CategoryResource), StatusCodes.Status200OK)]
		public async Task<ActionResult<CategoryResource>> Get(int id)
		{
			var category = await service.Get(id);
			if (category == null) return NotFound();
			var categoryResource = mapper.Map<Category, CategoryResource>(category);
			return Ok(categoryResource);
		}

		/// <summary>Create a new category</summary>
		/// <returns>A newly created category</returns>
		/// <response code="201">Returns the newly created category</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="categoryResource">The category to be added</param>
		[HttpPost]
		[ProducesResponseType(typeof(CategoryResource), StatusCodes.Status201Created)]
		public async Task<ActionResult<CategoryResource>> Create(SaveCategoryResource categoryResource)
		{
			var category = mapper.Map<SaveCategoryResource, Category>(categoryResource);
			var newCategory = await service.Create(category);
			var returnCategory = mapper.Map<Category, CategoryResource>(newCategory);
			return CreatedAtAction(nameof(GetAll), new { id = category.Id }, returnCategory);
		}

		/// <summary>Update a category</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Category was updated successfully</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="id">The id of the category</param>
		/// <param name="categoryResource">The category to be updated</param>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Update(int id, CategoryResource categoryResource)
		{
			if (id != categoryResource.Id) return BadRequest();
			var category = mapper.Map<CategoryResource, Category>(categoryResource);
			await service.Update(category);
			if (category == null) return NotFound();
			return NoContent();
		}

		/// <summary>Delete a category</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Category was deleted successfully</response>
		/// <response code="404">No category exists with the given id</response>
		/// <param name="id">The id of the category</param>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Delete(int id)
		{
			var category = await service.Delete(id);
			if (category == null) return NotFound();
			return NoContent();
		}
	}
}
