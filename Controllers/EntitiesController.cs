using AutoMapper;
using Homework1.DTOs.Entity;
using Homework1.Models;
using Homework1.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;

namespace Homework1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EntitiesController(IEntityService service, IMapper mapper) : ControllerBase
	{
		private readonly IEntityService _service = service;
		private readonly IMapper _mapper = mapper;

		[HttpGet]
		public ActionResult<IEnumerable<EntityReadDTO>> GetAll() => Ok(_service.GetAll());

		[HttpGet("{id:int}")]
		public ActionResult<EntityReadDTO> Get(int id)
		{
			var result = _service.GetById(id);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public ActionResult<EntityReadDTO> Create([FromBody] EntityCreateDTO entityCreateDTO)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var created = _service.Create(entityCreateDTO);
			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}

		[HttpPut("{id:int}")]
		public ActionResult<EntityReadDTO> Update(int id, [FromBody] EntityUpdateDTO entityUpdateDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updated = _service.Update(id, entityUpdateDTO);
			return updated is null ? NotFound() : Ok(updated);
		}

		[HttpPatch("{id:int}")]
		public ActionResult<EntityReadDTO> UpdatePartial(int id, 
											  [FromBody] JsonPatchDocument<EntityPatchDTO> jsonPatchDocument)
		{
			if (jsonPatchDocument is null)
			{
				return BadRequest();
			}

			var entity = _service.GetEntityById(id);

			if (entity is null)
			{
				return NotFound();
			}

			var entityPatchDto = _mapper.Map<EntityPatchDTO>(entity);

			jsonPatchDocument.ApplyTo(entityPatchDto, ModelState);

			if (!TryValidateModel(entityPatchDto))
			{
				return ValidationProblem(ModelState);
			}

			_mapper.Map(entityPatchDto, entity);
			return Ok(entity);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			_service.Delete(id);
			return NoContent();
		}
	}
}
