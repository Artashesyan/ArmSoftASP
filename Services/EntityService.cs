using AutoMapper;
using Homework1.DTOs.Entity;
using Homework1.Models;
using Homework1.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Services
{
	public class EntityService(IMapper mapper) : IEntityService
	{
		private readonly IMapper _mapper = mapper;
		private readonly List<Entity> _entities = [];
		private int _nextId = 1;

		public IEnumerable<EntityReadDTO> GetAll()
		{
			return _entities.Select(e => _mapper.Map<EntityReadDTO>(e));
		}

		public EntityReadDTO? GetById(int id)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			return entity == null ? null : _mapper.Map<EntityReadDTO>(entity);
			
		}

		public Entity? GetEntityById(int id)
		{
			return _entities.FirstOrDefault(e => e.Id == id);
		}

		public EntityReadDTO Create([FromBody] EntityCreateDTO createDTO)
		{
			var entity = _mapper.Map<Entity>(createDTO);

			entity.Id = _nextId++;
			_entities.Add(entity);

			return _mapper.Map<EntityReadDTO>(entity);
		}

		public EntityReadDTO? Update(int id, EntityUpdateDTO updateDTO)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			if (entity == null) return null;

			_mapper.Map(updateDTO, entity);
			return _mapper.Map<EntityReadDTO>(entity);
		}

		//public EntityReadDTO? UpdatePartial(int id, EntityPatchDTO dto)
		//{
		//	var entity = _entities.FirstOrDefault(e => e.Id == id);
		//	if (entity == null) return null;

		//	entity.Username = dto.Username is null ? entity.Username : dto.Username;
		//	entity.Email = dto.Email is null ? entity.Email : dto.Email;
		//	entity.Password = dto.Password is null ? entity.Password : dto.Password;
		//	entity.DateOfBirth = dto.DateOfBirth is null ? entity.DateOfBirth : dto.DateOfBirth;
		//	entity.Quantity = dto.Quantity is null ? entity.Quantity : (int)dto.Quantity;
		//	entity.Price = dto.Price is null ? entity.Price : (decimal)dto.Price;
		//	entity.Amount = dto.Amount is null ? entity.Amount : (int)dto.Amount;

		//	return _mapper.Map<EntityReadDTO>(entity);
		//}

		public bool Delete(int id)
		{
			var entity = _entities.FirstOrDefault(e => e.Id == id);
			if (entity == null) return false;
			_entities.Remove(entity);
			return true;
		}
	}

}
