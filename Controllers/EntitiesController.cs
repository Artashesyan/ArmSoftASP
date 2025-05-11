using Homework1.DTOs.Entity;
using Homework1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EntitiesController(IEntityService service) : ControllerBase
	{
		private readonly IEntityService _service = service;

		[HttpGet]
		public ActionResult<IEnumerable<EntityReadDTO>> GetAll() => Ok(_service.GetAll());

		[HttpGet("{id:int}")]
		public ActionResult<EntityReadDTO> Get(int id)
		{
			var result = _service.GetById(id);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public ActionResult<EntityReadDTO> Create(EntityCreateDTO dto)
		{
			var created = _service.Create(dto);
			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}

		[HttpPut("{id:int}")]
		public ActionResult<EntityReadDTO> Update(int id, EntityUpdateDTO dto)
		{
			var updated = _service.Update(id, dto);
			return updated is null ? NotFound() : Ok(updated);
		}

		[HttpPatch("{id:int}")]
		public ActionResult<EntityReadDTO> UpdatePartial(int id, EntityPatchDTO dto)
		{
			var updated = _service.UpdatePartial(id, dto);
			return updated is null ? NotFound() : Ok(updated);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			_service.Delete(id);
			return NoContent();
		}
	}
}
