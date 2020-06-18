using AutoMapper;
using DevEnvExam.Models;
using DevEnvExam.Resources;
using DevEnvExam.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEnvExam.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService service;
		private readonly IMapper mapper;

		public ProductsController(IProductService service, IMapper mapper)
		{
			this.service = service;
			this.mapper = mapper;
		}

		/// <summary>Lists all products</summary>
		/// <returns>A list of all products</returns>
		/// <response code="200">Returns all products</response>
		/// <param name="categoryId">Only return products with given category ID</param>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductResource>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ProductResource>>> GetAll(int? categoryId)
		{
			IEnumerable<Product> products;
			if (categoryId.HasValue) products = await service.GetByCategory((int)categoryId);
			else products = await service.GetAll();
			var productResources = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
			return Ok(productResources);
		}

		/// <summary>Get a single product</summary>
		/// <returns>A single product</returns>
		/// <response code="200">Returns the product with the given id</response>
		/// <response code="404">No product exists with the given id</response>
		/// <param name="id">The id of the product</param>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductResource), StatusCodes.Status200OK)]
		public async Task<ActionResult<ProductResource>> Get(int id)
		{
			var product = await service.Get(id);
			if (product == null) return NotFound();
			var productResource = mapper.Map<Product, ProductResource>(product);
			return Ok(productResource);
		}

		/// <summary>Create a new product</summary>
		/// <returns>A newly created product</returns>
		/// <response code="201">Returns the newly created product</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="productResource">The product to be added</param>
		[HttpPost]
		[ProducesResponseType(typeof(ProductResource), StatusCodes.Status201Created)]
		public async Task<ActionResult<ProductResource>> Create(SaveProductResource productResource)
		{
			var product = mapper.Map<SaveProductResource, Product>(productResource);
			var newProduct = await service.Create(product);
			var returnProduct = mapper.Map<Product, ProductResource>(newProduct);
			return CreatedAtAction(nameof(GetAll), new { id = product.Id }, returnProduct);
		}

		/// <summary>Update a product</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Product was updated successfully</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="id">The id of the product</param>
		/// <param name="productResource">The product to be updated</param>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Update(int id, ProductResource productResource)
		{
			if (id != productResource.Id) return BadRequest();
			var product = mapper.Map<ProductResource, Product>(productResource);
			await service.Update(product);
			if (product == null) return NotFound();
			return NoContent();
		}

		/// <summary>Delete a product</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Product was deleted successfully</response>
		/// <response code="404">No product exists with the given id</response>
		/// <param name="id">The id of the product</param>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Delete(int id)
		{
			var product = await service.Delete(id);
			if (product == null) return NotFound();
			return NoContent();
		}
	}
}
