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
	public class SuppliersController : ControllerBase
	{
		private readonly ISupplierService service;
		private readonly IMapper mapper;

		public SuppliersController(ISupplierService service, IMapper mapper)
		{
			this.service = service;
			this.mapper = mapper;
		}

		/// <summary>Lists all suppliers</summary>
		/// <returns>A list of all suppliers</returns>
		/// <response code="200">Returns all suppliers</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SupplierResource>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<SupplierResource>>> GetAll()
		{
			var supplier = await service.GetAll();
			var supplierResources = mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierResource>>(supplier);
			return Ok(supplierResources);
		}

		/// <summary>Get a single supplier</summary>
		/// <returns>A single supplier</returns>
		/// <response code="200">Returns the supplier with the given id</response>
		/// <response code="404">No supplier exists with the given id</response>
		/// <param name="id">The id of the supplier</param>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(SupplierResource), StatusCodes.Status200OK)]
		public async Task<ActionResult<SupplierResource>> Get(int id)
		{
			var supplier = await service.Get(id);
			if (supplier == null) return NotFound();
			var supplierResource = mapper.Map<Supplier, SupplierResource>(supplier);
			return Ok(supplierResource);
		}

		/// <summary>Create a new supplier</summary>
		/// <returns>A newly created supplier</returns>
		/// <response code="201">Returns the newly created supplier</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="supplierResource">The supplier to be added</param>
		[HttpPost]
		[ProducesResponseType(typeof(SupplierResource), StatusCodes.Status201Created)]
		public async Task<ActionResult<SupplierResource>> Create(SaveSupplierResource supplierResource)
		{
			var supplier = mapper.Map<SaveSupplierResource, Supplier>(supplierResource);
			var newSupplier = await service.Create(supplier);
			var returnSupplier = mapper.Map<Supplier, SupplierResource>(newSupplier);
			return CreatedAtAction(nameof(GetAll), new { id = supplier.Id }, returnSupplier);
		}

		/// <summary>Update a supplier</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Supplier was updated successfully</response>
		/// <response code="400">Item is null or JSON is malformed</response>
		/// <param name="id">The id of the supplier</param>
		/// <param name="supplierResource">The supplier to be updated</param>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Update(int id, SupplierResource supplierResource)
		{
			if (id != supplierResource.Id) return BadRequest();
			var supplier = mapper.Map<SupplierResource, Supplier>(supplierResource);
			await service.Update(supplier);
			if (supplier == null) return NotFound();
			return NoContent();
		}

		/// <summary>Delete a supplier</summary>
		/// <returns>Nothing</returns>
		/// <response code="204">Supplier was deleted successfully</response>
		/// <response code="404">No supplier exists with the given id</response>
		/// <param name="id">The id of the supplier</param>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Delete(int id)
		{
			var supplier = await service.Delete(id);
			if (supplier == null) return NotFound();
			return NoContent();
		}
	}
}
